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
            modelBuilder.Entity<TodoItem>()
            .HasOne(t => t.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<TodoItem> TodoItems {get; set;}
        public DbSet<User> Users {get; set;}
    }
}