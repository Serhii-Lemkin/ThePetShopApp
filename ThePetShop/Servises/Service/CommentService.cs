using ThePetShop.Servises.Interface;
using ThePetShopApp.Models;
using ThePetShopApp.Repositories;

namespace ThePetShop.Servises.Service
{
    public class CommentService : ICommentService
    {
        private readonly IDbDataRepository dbDataRepository;
        public CommentService(IDbDataRepository dbDataRepository) => this.dbDataRepository = dbDataRepository;
        public void AddCommentToAnimal(int id, string txt, string userId)
            => dbDataRepository.AddCommentToAnimal(
                new Comment
                {
                    AnimalId = id!,
                    CommentTxt = txt,
                    UserId = userId
                });

        public void DeleteComment(Comment comment) => dbDataRepository.RemoveComment(comment);

        public void DeleteComment(int id) => dbDataRepository.RemoveComment(GetCommentByID(id));

        public Comment GetCommentByID(int id) => dbDataRepository.GetComments().Single(x => x.CommentId == id);

        public IEnumerable<Comment> GetComments() => dbDataRepository.GetComments();
    }
}
