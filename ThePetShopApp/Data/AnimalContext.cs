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
        public List<Animal> GetMostPopular(int v) => AnimalList!
                .Include(a => a.Categories)
                .Include(a => a.Comments)
                .OrderByDescending(a => a.Comments!.Count)
                .Take(v)
                .ToList();

        public List<Category> GetCategories() => CategoryList!.ToList();
        public List<Animal> GetAnimalsWithCategories() => AnimalList!
                .Include(a => a.Categories)
                .Include(a => a.Comments)
                .ToList();
        public List<Animal> GetAnimalsOfCategoryByID(int? id) => AnimalList!
                .Where(a => a.CategoryId == id)
                .Include(a => a.Categories)
                .ToList();
        public Animal GetAnimalByID(int? id) => AnimalList!
                .Include(a => a.Categories!)
                .Include(a => a.Comments!)
                .FirstOrDefaultAsync(m => m.AnimalId! == id!).Result!;
    }
}
