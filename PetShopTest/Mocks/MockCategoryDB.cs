using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePetShopApp.Models;

namespace PetShopTest.Mocks
{
    public class MockCategoryDB
    {
        public List<Category> MockCategories { get; set; }
        public MockCategoryDB()
        {
            MockCategories = InitMock()!;
        }
        private List<Category> InitMock() => new List<Category> {
                new Category{ CategoryId = 1, Name = "Reptile" },
                new Category{ CategoryId = 2, Name = "Mammal" },
                new Category{ CategoryId = 3, Name = "Bird" },
                new Category{ CategoryId = 4, Name = "Insect" }
        };
    }
}
