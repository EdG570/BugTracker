using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using BugTracker.Application.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BugTracker.App.Controllers
{
    public class AppUserTests
    {
        [Fact]
        public async Task Collaborate_ShouldReturnViewResult_WhenInvoked()
        {
            // Arrange
            var mockProjectId = 1;
            var mockListCollabs = new List<Core.Models.AppUser>
            {
                new Core.Models.AppUser { Id = 1, Email = "email1@email.com", FirstName = "John", LastName = "Jones" },
                new Core.Models.AppUser { Id = 2, Email = "email2@email.com", FirstName = "Jim", LastName = "Smith" }
            };
            var mockListNonCollabs = new List<Core.Models.AppUser>
            {
                new Core.Models.AppUser { Id = 3, Email = "email3@email.com", FirstName = "Sam", LastName = "Johanson" },
                new Core.Models.AppUser { Id = 1, Email = "email1@email.com", FirstName = "John", LastName = "Jones" },
                new Core.Models.AppUser { Id = 2, Email = "email2@email.com", FirstName = "Jim", LastName = "Smith" }
            };
            var mockNotifications = new List<Core.Models.Notification>();
            var mockUserProjectService = new Mock<IUserProjectService>();
            var mockAppUserService = new Mock<IAppUserService>();
            var mockNotificationService = new Mock<INotificationService>();

            mockUserProjectService.Setup(x => x.GetUserCollabsByProjectId(It.IsAny<int>()))
                                  .ReturnsAsync(mockListCollabs);

            mockAppUserService.Setup(x => x.GetAll())
                              .ReturnsAsync(mockListNonCollabs);

            mockAppUserService.Setup(x => x.GetUserByClaim(It.IsAny<ClaimsPrincipal>()))
                              .ReturnsAsync(new Core.Models.AppUser());

            mockNotificationService.Setup(x => x.GetAllByUserId(It.IsAny<int>()))
                                   .ReturnsAsync(mockNotifications);

            var controller = new AppUserController(mockUserProjectService.Object, mockAppUserService.Object, mockNotificationService.Object);

            // Act
            var result = await controller.Collaborate(mockProjectId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}
