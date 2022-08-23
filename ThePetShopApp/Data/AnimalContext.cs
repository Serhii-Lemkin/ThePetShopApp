using Microsoft.EntityFrameworkCore;
using ThePetShopApp.Models;

namespace ThePetShopApp.Data
{
    public class AnimalContext : DbContext
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
        }

        public List<string> GetCategories() => (from c 
                                                in CategoryList 
                                                select c.Name).ToList();
        public string GetCategoryByID(int num) => CategoryList!.Single(x => x.CategoryId! == num).Name!;
        public int GetCategoryByName(string name) => CategoryList!.Single(x => x.Name! == name).CategoryId!;
    }
}
