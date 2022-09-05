using Moq;
using PetShopTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePetShop.Servises.Interface;
using ThePetShop.Servises.Service;
using ThePetShopApp.Repositories;

namespace PetShopTest
{
    public class FilteringServices_Tests
    {
        private readonly FilteringService _sut;
        private readonly MockAnimalDB mdb = new MockAnimalDB();
        private readonly Mock<IAnimalService> _mockService = new Mock<IAnimalService>();
        public FilteringServices_Tests()
        {
            _sut = new FilteringService(_mockService.Object);
        }
        [Fact]
        public void FilterAnimalsOfCategoryByID_ReturnsListOfOnlyOneCategory_TrueForSuccess()
        {
            _mockService.Setup(x => x.GetAnimals()).Returns(mdb.AnimalListMock);
            int cId = 1;

            var animals = _sut.FilterAnimalsOfCategoryByID(cId);

            Assert.True(animals.All(x => x.CategoryId == cId));
        }
        [Fact]
        public void FilterAnimalsByName_ReturnsTrueForSeccess()
        {
            _mockService.Setup(x => x.GetAnimals()).Returns(mdb.AnimalListMock);
            var s = "e";
            var animals = _sut.FilterAnimalsByName(s);
            Assert.True(animals.All(x => x.Name.Contains(s)));
        }
        [Fact]
        public void FilterAnimalsByName2_ReturnsTrueForSeccess()
        {
            _mockService.Setup(x => x.GetAnimals()).Returns(mdb.AnimalListMock);
            var s = "El";
            var animals = _sut.FilterAnimalsByName(s);
            Assert.True(animals.All(x => x.Name.Contains(s)) && animals.Count() == 1);
        }
        [Fact]
        public void FilterAnimals_ReturnsTrueForSeccess()
        {
            _mockService.Setup(x => x.GetAnimals()).Returns(mdb.AnimalListMock);
            var s = "e";
            int n = 1;
            var animals = _sut.FilterAnimals(n, s);
            Assert.True(animals.All(x => x.Name.ToLower().Contains(s.ToLower()) && x.CategoryId == n));
        }
    }
}
