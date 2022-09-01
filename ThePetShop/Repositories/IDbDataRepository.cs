using ThePetShopApp.Models;

namespace ThePetShopApp.Repositories
{
    public interface IDbDataRepository
    {
        public IEnumerable<Animal> GetAnimals();
        public IEnumerable<Comment> GetComments();
        public IEnumerable<Category> GetCategories();

        public void RemoveAnimal(Animal animal);
        public void RemoveComment(Comment comment);
        public void RemoveCategory(Category category);

        public void AddAnimal(Animal animal);
        public void AddCategory(Category category);
        public void AddCommentToAnimal(Comment comment);
        void Update(Animal animal);
    }
}
