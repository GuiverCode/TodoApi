using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    public class Usuario: Persona
    {
        public const int UsernameMaxLength = 64;
        public const int PasswordMaxLength = 5;


        public long Id{get; set;}

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username {get; set;}
        [Required]
        [JsonIgnore]
        public byte[] HashPassword { get; set; }

        
    }
}