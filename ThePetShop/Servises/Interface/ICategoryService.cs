using ThePetShopApp.Models;

namespace ThePetShop.Servises.Interface
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetCategories();
        void AddCategory(Category category);
        void RemoveCategory(Category category);
        public Category GetCategory(int id);
    }
}
