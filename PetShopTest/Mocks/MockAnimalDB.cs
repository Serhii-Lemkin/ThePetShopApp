using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePetShopApp.Models;

namespace PetShopTest.Mocks
{
    public class MockAnimalDB
    {
        public List<Animal> AnimalListMock { get; set; }
        public MockAnimalDB()
        {
            AnimalListMock = InitDbAnimalsMock();
        }
        public InvocationAction AddAnimalMock(Animal animal)
        {
            AnimalListMock.Add(animal);
            return new InvocationAction();
        }

        public void RemoveAnimalMock(Animal animal) => AnimalListMock.Remove(animal);
        List<Animal> InitDbAnimalsMock()
        {

            return new List<Animal> {
                new Animal {
                    AnimalId = 1,
                    Age = 5,
                    Name = "Elephant",
                    Description = "blabla Test1 blabla",
                    PictureName = "Test.jpg",
                    CategoryId = 2,
                },
                new Animal {
                    AnimalId = 2,
                    Age = 5,
                    Name = "Monkey",
                    Description = "blabla Test2 blabla",
                    PictureName = "Test.jpg",
                    CategoryId = 2,
                },
                new Animal {
                    AnimalId = 3,
                    Age = 5,
                    Name = "Bugger",
                    Description = "blabla Test3 blabla",
                    PictureName = "Test.jpg",
                    CategoryId = 2,
                }
            };

        }
    }
}
