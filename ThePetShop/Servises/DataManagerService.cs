using ThePetShopApp.Models;
using ThePetShopApp.Repositories;

namespace ThePetShopApp.Servises
{
    public class DataManagerService : IDataManagerService
    {
        private readonly IDbDataRepository dbDataRepository;

        public DataManagerService(IDbDataRepository dbDataRepository) => this.dbDataRepository = dbDataRepository;
        public Animal GetAnimalByID(int? id) => dbDataRepository!.GetAnimals().SingleOrDefault(x => x.AnimalId == id)!;
        public IEnumerable<Animal> GetAnimals() => dbDataRepository!.GetAnimals();

        public IEnumerable<Animal> GetAnimalsOfCategoryByID(int? id) => dbDataRepository!
            .GetAnimals()
            .Where(a => a.CategoryId == id);

        public IEnumerable<Category> GetCategories() => dbDataRepository!.GetCategories();

        public IEnumerable<Comment> GetComments() => dbDataRepository.GetComments();

        public IEnumerable<Animal> GetMostPopular(int num) => dbDataRepository!
            .GetAnimals()
            .OrderByDescending(a => a.Comments!.Count)
            .Take(num).ToList();
        public void AddCommentToAnimal(int id, string txt, string userId) => dbDataRepository.AddCommentToAnimal(new Comment { AnimalId = id!, CommentTxt = txt , UserId = userId });

        public void AddAnimal(Animal animal) => dbDataRepository.AddAnimal(animal);

        public void Update(Animal animal) => dbDataRepository.Update(animal);

        public void AddCategory(Category category) => dbDataRepository.AddCategory(category);

        public void Remove(Animal animal)
        {
            foreach (var comment in animal.Comments!) dbDataRepository.HaltRemoveComment(comment);
            dbDataRepository.RemoveAnimal(animal);

        }

        public void Remove(Comment comment) => dbDataRepository.RemoveComment(comment);

        public void Remove(Category category) => dbDataRepository.RemoveCategory(category);

        public Comment GetCommentByID(int id) => dbDataRepository.GetComments().Single(x => x.CommentId == id);

        public void DeleteComment(int id) => Remove(GetCommentByID(id));
    }
}
