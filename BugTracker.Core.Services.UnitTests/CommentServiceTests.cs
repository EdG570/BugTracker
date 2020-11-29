using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using BugTracker.Core.Models;
using BugTracker.Core.Interfaces;

namespace BugTracker.Core.Services.UnitTests
{
    public class CommentServiceTests
    {
        [Fact]
        public async Task Create_ReturnsOne_WhenCreateSuccess()
        {
            // Arrange
            var comment = new Comment 
            { 
                TicketId = 1,
                CreatedAt = DateTime.Now,
                CreatedBy = "Me",
                CreatorId = 3,
                Description = "This is a comment"
            };

            var mockCommentRepo = new Mock<ICommentRepository>();

            mockCommentRepo.Setup(x => x.Create(comment))
                           .ReturnsAsync(1);

            var service = new CommentService(mockCommentRepo.Object);

            // Act
            var result = await service.Create(comment);

            // Assert
            Assert.IsAssignableFrom<int>(result);
        }

        [Fact]
        public async Task Delete_ReturnsInt_AfterRepositoryDelete()
        {
            // Arrange
            var testId = 1;
            var mockCommentRepo = new Mock<ICommentRepository>();

            mockCommentRepo.Setup(x => x.Delete(testId))
                           .ReturnsAsync(1);

            var service = new CommentService(mockCommentRepo.Object);

            // Act
            var result = await service.Delete(testId);

            // Assert
            Assert.IsAssignableFrom<int>(result);
        }

        [Fact]
        public async Task FindOne_ReturnsComment_WhenCommentFoundById()
        {
            // Arrange
            var testId = 1;
            var mockCommentRepo = new Mock<ICommentRepository>();

            mockCommentRepo.Setup(x => x.FindOne(testId))
                           .ReturnsAsync(new Comment());

            var service = new CommentService(mockCommentRepo.Object);

            // Act
            var result = await service.FindOne(testId);

            // Assert
            Assert.IsAssignableFrom<Comment>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsIEnumerableOfComments_WhenInvoked()
        {
            // Arrange
            var mockCommentRepo = new Mock<ICommentRepository>();

            mockCommentRepo.Setup(x => x.GetAll())
                           .ReturnsAsync(new List<Comment>());

            var service = new CommentService(mockCommentRepo.Object);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Comment>>(result);
        }

        [Fact]
        public async Task Update_ReturnsInt_AfterRepoUpdate()
        {
            // Arrange
            var mockCommentRepo = new Mock<ICommentRepository>();

            mockCommentRepo.Setup(x => x.Update(It.IsAny<Comment>()))
                           .ReturnsAsync(1);

            var service = new CommentService(mockCommentRepo.Object);

            // Act
            var result = await service.Update(new Comment());

            // Assert
            Assert.IsAssignableFrom<int>(result);
        }

    }
}
