using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class Prioridad
    {
        public long Id {get; set;}

        [Required]
        public string Descripcion{get; set;}
    }
}