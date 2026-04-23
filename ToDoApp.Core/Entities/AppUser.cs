using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName => $"{Name} {Surname}".Trim();


        public ICollection<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();

    }
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
           
            builder.Ignore(x => x.FullName);




            // Email zaten Identity tarafından yönetiliyor
            // sadece ekstra alanlar burada tanımlanır
        }
    }
}
