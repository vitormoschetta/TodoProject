using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;
using Todo.Infrastructure.Database.Configurations;

namespace Todo.Infrastructure.Database.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                    : base(options) { }

        public DbSet<TodoItem> TodoItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoItemConfig());
        }
    }
}