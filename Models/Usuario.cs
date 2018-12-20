using System.ComponentModel.DataAnnotations;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    public class Usuario: Persona
    {
        public long Id{get; set;}

        [Required]
        public string Username {get; set;}
        [Required]
        public string Password {get; set;}
        
    }
}