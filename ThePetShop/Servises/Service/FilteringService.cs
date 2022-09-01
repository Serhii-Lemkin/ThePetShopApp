using ThePetShop.Servises.Interface;
using ThePetShopApp.Models;
using ThePetShopApp.Repositories;

namespace ThePetShop.Servises.Service
{
    public class FilteringService : IFilteringService
    {
        private readonly IDbDataRepository dbDataRepository;
        public FilteringService(IDbDataRepository dbDataRepository) => this.dbDataRepository = dbDataRepository;




        public IEnumerable<Animal> FilterAnimalsOfCategoryByID(int? id) => dbDataRepository!
            .GetAnimals()
            .Where(a => a.CategoryId == id);

        public IEnumerable<Animal> FilterAnimalsMostPopular(int num) => dbDataRepository!
            .GetAnimals()
            .OrderByDescending(a => a.Comments!.Count)
            .Take(num);

        public IEnumerable<Animal> FilterAnimalsByName(string inputString) => dbDataRepository!
            .GetAnimals()
            .Where(x => x.Name.Contains(inputString));

        public IEnumerable<Animal> FilterAnimals(int categoryID, string inputString)
        {
            var animals = dbDataRepository!.GetAnimals();
            if (categoryID != 0)
                animals = animals.Where(x => x.CategoryId == categoryID);
            if (!String.IsNullOrEmpty(inputString) && !String.IsNullOrWhiteSpace(inputString))
                animals = animals.Where(x => x.Name.Contains(inputString));
            return animals;
        }
    }
}
