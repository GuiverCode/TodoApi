using Microsoft.EntityFrameworkCore;
using TodoApi.Controllers;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        //crea el contexto de la BD
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>()
            .HasOne(t => t.Usuario)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Tarea> Tareas {get; set;}
        public DbSet<Usuario> Usuarios {get; set;}
    }
}