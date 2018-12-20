using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using TodoApi.Controllers;

namespace TodoApi.Models
{
    public class Tarea
    {
        public long Id { get; set; }
        
        [Required] 
        public string Nombre { get; set; }
        public bool Completado { get; set; } = false;
        [ForeignKey("IdUsuario")]
        [JsonIgnore]
        public Usuario Usuario {get; set;}
        public long? IdUsuario {get; set;} 
    }
}