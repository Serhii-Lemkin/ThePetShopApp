using ThePetShop.Servises.Interface;
using ThePetShopApp.Models;
using ThePetShopApp.Repositories;

namespace ThePetShop.Servises.Service
{
    public class AnimalService : IAnimalService
    {
        private readonly IDbDataRepository dbDataRepository;

        public AnimalService(IDbDataRepository dbDataRepository) => this.dbDataRepository = dbDataRepository;

        public void AddAnimal(Animal animal) => dbDataRepository.AddAnimal(animal);

        public Animal GetAnimalByID(int? id) => dbDataRepository.GetAnimals().Single(x => x.AnimalId == id)!;

        public IEnumerable<Animal> GetAnimals() => dbDataRepository!.GetAnimals();

        public void RemoveAnimal(Animal animal) => dbDataRepository.RemoveAnimal(animal);

        public void Update(Animal animal) => dbDataRepository.Update(animal);
    }
}
