using System.ComponentModel.DataAnnotations;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    public class User: Person
    {
        public long Id{get; set;}

        [Required]
        public string Username {get; set;}
        [Required]
        public string Password {get; set;}
        
    }
}