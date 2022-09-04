using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePetShopApp.Models;

namespace PetShopTest.Mocks
{
    public class MockCommentDB
    {
        public List<Comment> MockComments { get; set; }
        public MockCommentDB() => MockComments = InitMock()!;
        public void RemoveCommentMock(Comment comment)
        {
            MockComments.Remove(comment);
        }
        public void AddCommentMock(Comment comment)
            => MockComments.Add(comment);

        private List<Comment>? InitMock() => new List<Comment>{
        new Comment { UserId = "1", CommentId = 1, CommentTxt = "1 comment", AnimalId = 1},
        new Comment { UserId = "2", CommentId = 2, CommentTxt = "2 comment", AnimalId = 2},
        new Comment { UserId = "1", CommentId = 3, CommentTxt = "3 comment", AnimalId = 1},
        new Comment { UserId = "2", CommentId = 4, CommentTxt = "4 comment", AnimalId = 3},
        new Comment { UserId = "1", CommentId = 5, CommentTxt = "5 comment", AnimalId = 1},
        new Comment { UserId = "1", CommentId = 6, CommentTxt = "6 comment", AnimalId = 3},
        new Comment { UserId = "3", CommentId = 7, CommentTxt = "7 comment", AnimalId = 1},
        new Comment { UserId = "1", CommentId = 8, CommentTxt = "8 comment", AnimalId = 2},
        new Comment { UserId = "3", CommentId = 9, CommentTxt = "9 comment", AnimalId = 1},
    };

    }
}
