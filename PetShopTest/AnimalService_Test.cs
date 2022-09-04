using Moq;
using ThePetShop.Servises.Service;
using ThePetShopApp.Models;
using ThePetShopApp.Repositories;
using PetShopTest.Mocks;

namespace PetShopTest
{
    public class AnimalService_Test
    {
        private readonly AnimalService _sut;
        private readonly MockAnimalDB mdb = new MockAnimalDB();
        private readonly Mock<IDbDataRepository> _mockRepository = new Mock<IDbDataRepository>();
        public AnimalService_Test()
        {
            _sut = new AnimalService(_mockRepository.Object);
            
        }

        [Fact]
        public void GatAnimals_ListNotNull_ReturnsTrueIfNotNull()
        {
            _mockRepository.Setup(x => x.GetAnimals()).Returns(mdb.AnimalListMock);

            var animals = _sut.GetAnimals();
           
            Assert.NotNull(animals);
        }
        [Fact]
        public void GetAnimalByAnimalID_ReturnsTrue_IdEqualsAnimalId()
        {
            int id = 2;
            _mockRepository.Setup(x => x.GetAnimals()).Returns(mdb.AnimalListMock);

            Animal animal = _sut.GetAnimalByID(id);

            Assert.True(id == animal.AnimalId);
        }
        [Fact]
        public void AddAnimal_ReturnsTrue_ListHasNewID()
        {
            var animal = new Animal
            {
                AnimalId = 4,
                Age = 5,
                Name = "Elephant",
                Description = "blabla Test4 blabla",
                PictureName = "Test.jpg",
                CategoryId = 2,
            };
            _mockRepository.Setup(x => x.AddAnimal(animal)).Callback(() => mdb.AddAnimalMock(animal));

            _sut.AddAnimal(animal);

            Assert.True(mdb.AnimalListMock.Count == 4 && mdb.AnimalListMock[3].AnimalId == 4);
        }
        [Fact]
        void RemoveAnimal_ReturnsTrue_ListDontCountainIdOfDeletedAnimalCountIsSmaller()
        {
            int prevCount = mdb.AnimalListMock.Count;
            var animal = mdb.AnimalListMock.First();
            _mockRepository.Setup(x => x.RemoveAnimal(animal)).Callback(() => mdb.RemoveAnimalMock(animal));

            _sut.RemoveAnimal(animal);

            Assert.True(mdb.AnimalListMock.Count == prevCount-1 && !mdb.AnimalListMock.Any(x=>x.AnimalId == animal.AnimalId));
        }



    }

}