using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Models;


namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
  public class AuthController : Controller
  {
        private readonly TodoContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(TodoContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto credenciales)
        {
            if (credenciales == null)
            {
                return BadRequest(new {Error = "Debe enviar credenciales"});
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var usuario =
                await _context.Usuarios.FirstOrDefaultAsync(u => u.Username.Equals(credenciales.Username));
            if (usuario == null)
            {
                return Unauthorized();
            }

            using (SHA512 shaEncrypter = new SHA512Managed())
            {
                var hashed = shaEncrypter.ComputeHash(
                    Encoding.UTF8.GetBytes(credenciales.Password)
                );

                if (!Enumerable.SequenceEqual(usuario.HashPassword, hashed))
                {
                    return Unauthorized();
                }
            }
            return Ok(new {Token = GenerateJwtToken(usuario), Usuario = usuario});
        }



        private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));
            

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
