using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController: ControllerBase
    {
        private readonly TodoContext _context;

        public UsuarioController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetAll()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("{id}", Name="GetUsuario")]
        public async Task<ActionResult<Usuario>> GetById(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if(usuario == null) return NotFound();

            return usuario;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioDto usDto)
        {
            if(await _context.Usuarios
                .AnyAsync(u => u.Username == usDto.Username))
                return BadRequest(new {Error ="El username ya existe"});
            if(usDto.Password1 == null) return BadRequest(new {Error ="Debe ingresar la contraseña"});
            if(usDto.Password2 == null) return BadRequest(new {Error ="Debe confirmar la contraseña"});
            if(!usDto.Password1.Equals(usDto.Password2)) return BadRequest(new {Error ="Las contraseñas no coinciden"});

            Usuario usuario = new Usuario();
            usuario.HashPassword = GenerateHash(usDto.Password1);
            usuario.Username = usDto.Username;
            usuario.Nombre = usDto.Nombre;
            usuario.Apellido = usDto.Apellido;
            usuario.FechaNacimiento = usDto.FechaNacimiento?.Date;

            _context.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetUsuario", new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UsuarioDto usDto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if(usuario == null) return NotFound();

            if(await _context.Usuarios
                .AnyAsync(u => u.Username == usDto.Username && u.Id != id))
                return BadRequest(new {Error ="El username ya existe"});

            if(usDto.Password1 != null && usDto.Password2 != null)
            {
                if(!usDto.Password1.Equals(usDto.Password2)) return BadRequest(new {Error ="Los passwords no coinciden"});
                usuario.HashPassword = GenerateHash(usDto.Password1);
            }
            
            usuario.Username = usDto.Username;
            usuario.Nombre = usDto.Nombre;
            usuario.Apellido = usDto.Apellido;
            usuario.FechaNacimiento = usDto.FechaNacimiento?.Date;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if(usuario == null) return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private byte[] GenerateHash(string pass)
        {
            if (!string.IsNullOrEmpty(pass))
            {
                using (SHA512 shaEncrypter = new SHA512Managed())
                {
                    var hashed = shaEncrypter.ComputeHash(
                        Encoding.UTF8.GetBytes(pass));
                    return hashed;
                }
            }

            return null;
        }

    }

    public class UsuarioDto
    {
        [Required]
        public string Username {get; set;}
        public string Password1 {get; set;}
        public string Password2 {get; set;}
        public string Nombre {get; set;}
        public string Apellido {get; set;}
        public DateTime? FechaNacimiento {get; set;}
    }
}