using ThePetShopApp.Models;

namespace ThePetShop.Servises.Service
{
    public class CashStorage
    {
        private static CashStorage instance;
        public static CashStorage Instance => instance ?? (instance = new CashStorage());
        CashStorage()
        {

        }
        public bool AnimalChanged { get; set; }
        public bool CommentChanged { get; set; }
        public bool CategoryChanged { get; set; }

        public IEnumerable<Animal> AnimalCash { get; set; }
        public IEnumerable<Comment> CommentCash { get; set; }
        public IEnumerable<Category> CategoryCash { get; set; }

        public void StoreAnimals(IEnumerable<Animal> animalCash) => AnimalCash = animalCash;
        public void StoreComments(IEnumerable<Comment> commentCash) => CommentCash = commentCash;
        public void StoreCategories(IEnumerable<Category> categoryCash) => CategoryCash = categoryCash;
    }
}
