using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ThePetShopApp.Models;
using ThePetShopApp.Servises;

namespace ThePetShopApp.Data
{
    public class AnimalContext : IdentityDbContext
    {
        public AnimalContext(DbContextOptions<AnimalContext> options) : base(options)
        {

        }
        public DbSet<Animal>? AnimalList { get; set; }
        public DbSet<Comment>? CommentList { get; set; }
        public DbSet<Category>? CategoryList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new { CategoryId = 1, Name = "Reptile" },
                new { CategoryId = 2, Name = "Mammal" },
                new { CategoryId = 3, Name = "Bird" },
                new { CategoryId = 4, Name = "Insect" }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
