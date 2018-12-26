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

            //Poblar tablas con datos semillas
            modelBuilder.Entity<Prioridad>()
            .HasData( 
                new Prioridad{Id = 1, Descripcion = "Baja"},
                new Prioridad{Id = 2, Descripcion = "Media"},
                new Prioridad{Id = 3, Descripcion = "Alta"}
                );

            //Configuracion del delete (Cascade, Restrict)
            modelBuilder.Entity<Tarea>()
            .HasOne(t => t.Usuario)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tarea>()
            .HasOne(t => t.Prioridad)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Tarea> Tareas {get; set;}
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Prioridad> Prioridades {get; set;}
    }
}