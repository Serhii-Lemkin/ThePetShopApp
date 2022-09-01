using ThePetShopApp.Models;

namespace ThePetShop.Servises.Interface
{
    public interface IAnimalService
    {
        public IEnumerable<Animal> GetAnimals();
        void AddAnimal(Animal animal);
        void Update(Animal animal);
        void RemoveAnimal(Animal animal);
        public Animal GetAnimalByID(int? id);

    }
}
