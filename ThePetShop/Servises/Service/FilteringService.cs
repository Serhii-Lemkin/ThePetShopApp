using ThePetShop.Servises.Interface;
using ThePetShopApp.Models;
using ThePetShopApp.Repositories;

namespace ThePetShop.Servises.Service
{
    public class FilteringService : IFilteringService
    {
        private readonly IAnimalService animalService;
        public FilteringService(IAnimalService animalService) => this.animalService = animalService;

        public IEnumerable<Animal> FilterAnimalsOfCategoryByID(int? id) => animalService!
            .GetAnimals()
            .Where(a => a.CategoryId == id);

        public IEnumerable<Animal> FilterAnimalsMostPopular(int num) => animalService!
            .GetAnimals()
            .OrderByDescending(a => a.Comments!.Count)
            .Take(num);

        public IEnumerable<Animal> FilterAnimalsByName(string inputString) => animalService!
            .GetAnimals()
            .Where(x => x.Name.ToLower().Contains(inputString.ToLower()));

        public IEnumerable<Animal> FilterAnimals(int categoryID, string inputString)
        {
            var animals = animalService!.GetAnimals();
            if (categoryID != 0)
                animals = animals.Where(x => x.CategoryId == categoryID);
            if (!String.IsNullOrEmpty(inputString) && !String.IsNullOrWhiteSpace(inputString))
                animals = animals.Where(x => x.Name.ToLower().Contains(inputString.ToLower()));
            return animals;
        }
    }
}
