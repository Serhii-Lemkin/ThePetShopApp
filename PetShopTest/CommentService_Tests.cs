using Moq;
using PetShopTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePetShop.Servises.Service;
using ThePetShopApp.Models;
using ThePetShopApp.Repositories;

namespace PetShopTest
{
    public class CommentService_Tests
    {
        private readonly CommentService _sut;
        private readonly MockCommentDB mCommentdb = new MockCommentDB();
        private readonly Mock<IDbDataRepository> _mockRepository = new Mock<IDbDataRepository>();
        public CommentService_Tests()
        {
            _sut = new CommentService(_mockRepository.Object);
        }

        [Fact]
        public void GetComments_ReturnsTrueIfNotNull()
        {
            _mockRepository.Setup(x => x.GetComments()).Returns(mCommentdb.MockComments);

            Assert.True(_sut.GetComments() != null && _sut.GetComments().Count() > 0);
        }
        [Fact]
        public void RemoveComment_CommentWasRemoved()
        {
            var comment = mCommentdb.MockComments.First();
            _mockRepository.Setup(x => x.RemoveComment(comment)).Callback(() => mCommentdb.RemoveCommentMock(comment));

            _sut.DeleteComment(comment);

            Assert.True(mCommentdb!.MockComments.All(x => comment.CommentId != x.CommentId));
        }

    }
}
