using Microsoft.EntityFrameworkCore;
using ThePetShop.Servises.Service;
using ThePetShopApp.Data;
using ThePetShopApp.Models;

namespace ThePetShopApp.Repositories
{
    public class DbDataRepository : IDbDataRepository
    {
        private readonly AnimalContext context;
        public DbDataRepository(AnimalContext _context)
        {
            context = _context;
        }


        //                        Get functions
        #region Upload functions
        public IEnumerable<Animal> GetAnimals() => context.AnimalList!
                  .Include(a => a.Categories!)
                  .Include(a => a.Comments!)
                      .ThenInclude(c => c.user)!;

        public IEnumerable<Category> GetCategories() => context.CategoryList!;

        public IEnumerable<Comment> GetComments() => context.CommentList!.Include(a => a.user);
        #endregion
        //                        Add Functions
        #region Add Functions
        public void AddAnimal(Animal animal)
        {
            context.AnimalList!.Add(animal);
            context.SaveChanges();
        }
        public void AddCategory(Category category)
        {
            context.CategoryList!.Add(category);
            context.SaveChanges();
        }
        public void AddCommentToAnimal(Comment comment)
        {
            context.CommentList!.Add(comment);
            context.SaveChanges();
        }
        #endregion
        //                        Remove functions
        #region Remove functions
        public void RemoveAnimal(Animal animal)
        {
            context.AnimalList!.Remove(animal);
            context.SaveChanges();
        }

        public void RemoveCategory(Category category)
        {
            context.CategoryList!.Remove(category);
            context.SaveChanges();
        }

        public void RemoveComment(Comment comment)
        {
            context.CommentList!.Remove(comment);
            context.SaveChanges();
        }
        #endregion
        //                        Update Function
        #region Update functions
        public void Update(Animal animal)
        {
            context.Update(animal);
            context.SaveChanges();
        }
        #endregion

    }
}
