using ThePetShopApp.Models;

namespace ThePetShop.Servises.Interface
{
    public interface ICommentService
    {
        public IEnumerable<Comment> GetComments();
        void DeleteComment(Comment comment);
        public void DeleteComment(int id);
        public Comment GetCommentByID(int id);
        public void AddCommentToAnimal(int id, string txt, string userId);
    }
}