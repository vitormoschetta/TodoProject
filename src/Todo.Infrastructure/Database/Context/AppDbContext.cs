using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Infrastructure.Database.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                    : base(options) { }

        public DbSet<TodoItem> TodoItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}