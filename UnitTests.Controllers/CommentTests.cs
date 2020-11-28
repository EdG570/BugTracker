using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using BugTracker.Core.Interfaces;
using BugTracker.Application.Controllers;
using System.Security.Claims;
using BugTracker.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.App.Controllers
{
    public class CommentTests
    {
        [Theory]
        [InlineData("", "This is a description.")]
        [InlineData("1", "")]
        public async Task Create_ShouldThrowArgumentException_WhenAtLeastOneArgIsNullOrEmpty(string ticketId, string description)
        {
            // Arrange
            var mockCommentService = new Mock<ICommentService>();
            var mockAppUserService = new Mock<IAppUserService>();

            var controller = new CommentController(mockCommentService.Object, mockAppUserService.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => controller.Create(ticketId, description));
        }

        [Theory]
        [InlineData("2", "This is a description.")]
        [InlineData("1", "This is also a description")]
        public async Task Create_ShouldReturnJsonResult_WhenValidArgsArePassed(string ticketId, string description)
        {
            // Arrange
            var mockCommentService = new Mock<ICommentService>();
            var mockAppUserService = new Mock<IAppUserService>();

            mockAppUserService.Setup(x => x.GetUserByClaim(It.IsAny<ClaimsPrincipal>()))
                              .ReturnsAsync(new Core.Models.AppUser());

            mockCommentService.Setup(x => x.Create(It.IsAny<Comment>()))
                              .ReturnsAsync(1);

            var controller = new CommentController(mockCommentService.Object, mockAppUserService.Object);

            // Act
            var result = await controller.Create(ticketId, description);

            // Assert
            var viewResult = Assert.IsType<JsonResult>(result);
        }
    }
}
