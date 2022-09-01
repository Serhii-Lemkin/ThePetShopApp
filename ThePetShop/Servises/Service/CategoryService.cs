using ThePetShop.Servises.Interface;
using ThePetShopApp.Models;
using ThePetShopApp.Repositories;

namespace ThePetShop.Servises.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IDbDataRepository dbDataRepository;

        public CategoryService(IDbDataRepository dbDataRepository) => this.dbDataRepository = dbDataRepository;
        public IEnumerable<Category> GetCategories() => dbDataRepository!.GetCategories();

        public void AddCategory(Category category) => dbDataRepository.AddCategory(category);
        public void RemoveCategory(Category category) => dbDataRepository.RemoveCategory(category);

        public Category GetCategory(int id) => GetCategories().SingleOrDefault(x => x.CategoryId == id)!;
    }
}
