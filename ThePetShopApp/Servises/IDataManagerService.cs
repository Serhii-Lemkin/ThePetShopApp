using ThePetShopApp.Models;
using ThePetShopApp.Repositories;

namespace ThePetShopApp.Servises
{
    public interface IDataManagerService
    {
        public IEnumerable<Animal> GetAnimals();
        public IEnumerable<Comment> GetComments();
        public IEnumerable<Animal> GetMostPopular(int num);
        public IEnumerable<Category> GetCategories();
        public IEnumerable<Animal> GetAnimalsOfCategoryByID(int? id);
        public Animal GetAnimalByID(int? id);
        public void AddCommentToAnimal(int id, string txt);
        void AddAnimal(Animal animal);
        void AddCategory(Category category);
        void Update(Animal animal);
        void Remove(Animal animal);
        void Remove(Comment comment);
        void Remove(Category category);
        public void DeleteComment(int id);
        public Comment GetCommentByID(int id);
    }
}
