using ThePetShopApp.Models;

namespace ThePetShop.Servises.Interface
{
    public interface IFilteringService
    {
        public IEnumerable<Animal> FilterAnimalsMostPopular(int num);
        public IEnumerable<Animal> FilterAnimalsOfCategoryByID(int? id);
        public IEnumerable<Animal> FilterAnimalsByName(string inputName);
        public IEnumerable<Animal> FilterAnimals(int id, string inputString, string inputSpesies);
    }
}
