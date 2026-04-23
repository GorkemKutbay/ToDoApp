using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Entities;

namespace ToDoApp.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public ICollection<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
    }


}
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(450);

        // aynı kullanıcı aynı kategori adını tekrar ekleyemesin
        builder.HasIndex(x => new { x.UserId, x.Name })
            .IsUnique();
        //builder.HasOne(x => x.User)
        //    .WithMany()
        //    .HasForeignKey(x => x.UserId)
        //    .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
            .WithMany(u => u.Categories)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
