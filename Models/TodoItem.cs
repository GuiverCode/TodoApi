using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using TodoApi.Controllers;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        
        [Required] 
        public string Name { get; set; }
        public bool IsComplete { get; set; } = false;
        [ForeignKey("UserId")]
        [JsonIgnore]
        public User User {get; set;}
        public long? UserId {get; set;} 
    }
}