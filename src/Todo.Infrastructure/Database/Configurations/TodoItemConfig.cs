using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Models;

namespace Todo.Infrastructure.Database.Configurations
{
    public class TodoItemConfig : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.ToTable("todo_item");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");                

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("title");

            builder.Property(x => x.Done)
                .IsRequired()
                .HasColumnName("done");
        }
    }
}