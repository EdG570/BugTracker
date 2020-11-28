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
    public class NotificationTests
    {
        [Fact]
        public async Task Index_ReturnsViewResult_WhenInvoked()
        {
            // Arrange
            var mockNotificationService = new Mock<INotificationService>();
            var mockAppUserService = new Mock<IAppUserService>();
            var mockProjectService = new Mock<IProjectService>();
            var mockUserProjectService = new Mock<IUserProjectService>();

            mockNotificationService.Setup(x => x.GetAll())
                                   .ReturnsAsync(new List<Core.Models.Notification>());

            var controller = new NotificationController(mockAppUserService.Object, mockProjectService.Object, mockNotificationService.Object, mockUserProjectService.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Theory]
        [InlineData(null, 1)]
        public async Task Create_ReturnsRedirectToActionResult_WhenCollaboratorsArgIsNullOrCountIsZero(List<string> selectedCollaborators, int projectId)
        {
            // Arrange
            var mockNotificationService = new Mock<INotificationService>();
            var mockAppUserService = new Mock<IAppUserService>();
            var mockProjectService = new Mock<IProjectService>();
            var mockUserProjectService = new Mock<IUserProjectService>();

            var controller = new NotificationController(mockAppUserService.Object, mockProjectService.Object, mockNotificationService.Object, mockUserProjectService.Object);

            // Act
            var result = await controller.Create(selectedCollaborators, projectId);

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsRedirectToActionResult_WhenInvoked()
        {
            // Arrange
            var user = new Core.Models.AppUser { Id = 1, FirstName = "Joe", LastName = "Jones" };
            var project = new Core.Models.Project { Id = 1, Name = "Project 1" };
            var projectId = 1;
            var selectedCollaborators = new List<string> { "2", "3" };

            var mockNotificationService = new Mock<INotificationService>();
            var mockAppUserService = new Mock<IAppUserService>();
            var mockProjectService = new Mock<IProjectService>();
            var mockUserProjectService = new Mock<IUserProjectService>();

            mockProjectService.Setup(x => x.FindOne(projectId))
                              .ReturnsAsync(project);

            mockAppUserService.Setup(x => x.GetUserByClaim(It.IsAny<ClaimsPrincipal>()))
                              .ReturnsAsync(user);

            mockNotificationService.Setup(x => x.Create(It.IsAny<Notification>()))
                                   .ReturnsAsync(1);

            var controller = new NotificationController(mockAppUserService.Object, mockProjectService.Object, mockNotificationService.Object, mockUserProjectService.Object);

            // Act 
            var result = await controller.Create(selectedCollaborators, projectId);

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Acknowledge_ThrowsKeyNotFoundException_WhenNotificationIsNotFoundById()
        {
            // Arrange
            var isAccepted = true;
            var notificationId = 34;

            var mockNotificationService = new Mock<INotificationService>();
            var mockAppUserService = new Mock<IAppUserService>();
            var mockProjectService = new Mock<IProjectService>();
            var mockUserProjectService = new Mock<IUserProjectService>();

            mockNotificationService.Setup(x => x.FindOne(notificationId))
                                   .ReturnsAsync((Core.Models.Notification)null);

            var controller = new NotificationController(mockAppUserService.Object, mockProjectService.Object, mockNotificationService.Object, mockUserProjectService.Object);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => controller.Acknowledge(isAccepted, notificationId));
        }

        [Fact]
        public async Task Acknowledge_ReturnsJsonResult_WhenNotificationIsFoundById()
        {
            // Arrange
            var isAccepted = true;
            var notificationId = 34;

            var mockNotificationService = new Mock<INotificationService>();
            var mockAppUserService = new Mock<IAppUserService>();
            var mockProjectService = new Mock<IProjectService>();
            var mockUserProjectService = new Mock<IUserProjectService>();

            mockNotificationService.Setup(x => x.FindOne(notificationId))
                                   .ReturnsAsync(new Core.Models.Notification());

            var controller = new NotificationController(mockAppUserService.Object, mockProjectService.Object, mockNotificationService.Object, mockUserProjectService.Object);

            // Act 
            var result = await controller.Acknowledge(isAccepted, notificationId);

            // Assert
            var viewResult = Assert.IsType<JsonResult>(result);
        }
    }
}
