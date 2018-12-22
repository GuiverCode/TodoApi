using System;
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
        public string Descripcion {get; set;}
        public bool Completado { get; set; } = false;
        public DateTime? FechaVencimiento {get; set;}

        [ForeignKey("IdUsuario")]
        [JsonIgnore]
        public Usuario Usuario {get; set;}
        public long? IdUsuario {get; set;}

        [ForeignKey("IdPrioridad")]
        [JsonIgnore]
        public Prioridad Prioridad {get; set;}
        public long? IdPrioridad {get; set;} 
    }
}