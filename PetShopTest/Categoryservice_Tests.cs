using Moq;
using PetShopTest.Mocks;
using ThePetShop.Servises.Service;
using ThePetShopApp.Models;
using ThePetShopApp.Repositories;

namespace PetShopTest
{
    public class Categoryservice_Tests
    {
        private readonly CategoryService _sut;
        private readonly MockCategoryDB mdb = new MockCategoryDB();
        private readonly Mock<IDbDataRepository> _mockRepository = new Mock<IDbDataRepository>();
        public Categoryservice_Tests()
        {
            _sut = new CategoryService(_mockRepository.Object);
        }
        [Fact]
        public void CategoryListNotNull_ReturnsTrue()
        {
            _mockRepository.Setup(x => x.GetCategories()).Returns(mdb.MockCategories);

            var categories = _sut.GetCategories();

            Assert.True(categories != null && categories.Count() == 4);
        }
        [Fact]
        public void GetCategoryByID_ReturnsTrueIfReturnedCategoryIdIsSameAsGiven()
        {
            int id = 2;
            _mockRepository.Setup(x => x.GetCategories()).Returns(mdb.MockCategories);

            Category category = _sut.GetCategory(id);

            Assert.True(id == category.CategoryId);

        }
        //GetCategory id
    }
}
