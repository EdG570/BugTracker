using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using BugTracker.Application.Facades;
using BugTracker.Application.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BugTracker.Core.Models;
using BugTracker.Application.ViewModels.ProjectViewModels;

namespace BugTracker.App.Controllers
{
    public class ProjectTests
    {
        [Fact]
        public async Task Index_ReturnsViewResult_WhenInvoked()
        {
            // Arrange
            var season = "fall";
            var mockProjectFacade = new Mock<IProjectFacade>();

            mockProjectFacade.Setup(x => x.GetUser(It.IsAny<ClaimsPrincipal>()))
                             .ReturnsAsync(new Core.Models.AppUser());

            mockProjectFacade.Setup(x => x.GetSeason())
                             .Returns(season);

            var controller = new ProjectController(mockProjectFacade.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Index_ReturnsIndexViewModel_WhenInvoked()
        {
            // Arrange
            var season = "fall";
            var mockProjectFacade = new Mock<IProjectFacade>();

            mockProjectFacade.Setup(x => x.GetUser(It.IsAny<ClaimsPrincipal>()))
                             .ReturnsAsync(new Core.Models.AppUser());

            mockProjectFacade.Setup(x => x.GetSeason())
                             .Returns(season);

            var controller = new ProjectController(mockProjectFacade.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IndexViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task CreatePost_ReturnsRedirectToActionResult_WhenModelStateInvalid()
        {
            // Arrange
            var invalidIndexViewModel = new IndexViewModel { NewProjectName = null };
            var mockProjectFacade = new Mock<IProjectFacade>();

            mockProjectFacade.Setup(x => x.GetUser(It.IsAny<ClaimsPrincipal>()))
                             .ReturnsAsync(new Core.Models.AppUser());

            var controller = new ProjectController(mockProjectFacade.Object);
            controller.ModelState.AddModelError("Error", "Model State Invalid");

            // Act
            var result = await controller.Create(invalidIndexViewModel);

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task CreatePost_ReturnsRedirectToActionResult_WhenProjectCreateSuccess()
        {
            // Arrange
            var validViewModel = new IndexViewModel
            {
                NewProjectDescription = "This is a project description.",
                NewProjectName = "Project Z"
            };

            var mockProjectFacade = new Mock<IProjectFacade>();

            mockProjectFacade.Setup(x => x.GetUser(It.IsAny<ClaimsPrincipal>()))
                             .ReturnsAsync(new Core.Models.AppUser());

            mockProjectFacade.Setup(x => x.CreateProject(It.IsAny<Project>()))
                             .ReturnsAsync(1);

            mockProjectFacade.Setup(x => x.CreateUserProject(It.IsAny<UserProject>()))
                             .ReturnsAsync(1);

            var controller = new ProjectController(mockProjectFacade.Object);

            // Act
            var result = await controller.Create(validViewModel);

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Detail_ThrowsKeyNotFoundException_WhenProjectNotFound()
        {
            // Arrange
            var mockProjectFacade = new Mock<IProjectFacade>();

            mockProjectFacade.Setup(x => x.FindProjectById(It.IsAny<int>()))
                             .Throws(new KeyNotFoundException());

            var controller = new ProjectController(mockProjectFacade.Object);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => controller.Detail(0));
        }

        [Fact]
        public async Task Detail_ReturnsViewResult_WhenProjectIsFound()
        {
            // Arrange
            var mockProjectFacade = new Mock<IProjectFacade>();

            mockProjectFacade.Setup(x => x.FindProjectById(It.IsAny<int>()))
                             .ReturnsAsync(new Core.Models.Project());

            mockProjectFacade.Setup(x => x.GetNotificationsByUserId(It.IsAny<int>()))
                             .ReturnsAsync(new List<Core.Models.Notification>());

            var controller = new ProjectController(mockProjectFacade.Object);

            // Act
            var result = await controller.Detail(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Theory]
        [InlineData(null, "Description", "url", "1")]
        [InlineData("Project 1", "", "url", "1")]
        [InlineData("Project 1", "Description", "", null)]
        public async Task Edit_ThrowsArgumentException_WhenArgumentsAreNull(string name, string description, string link, string projectId)
        {
            // Arrange
            var mockProjectFacade = new Mock<IProjectFacade>();
            var controller = new ProjectController(mockProjectFacade.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => controller.Edit(name, description, link, projectId));
        }

        [Theory]
        [InlineData("Project 3", "Description 3", "url", "3")]
        [InlineData("Project 1", "Description 1", "url", "1")]
        [InlineData("Project 2", "Description 2", "", "2")]
        public async Task Edit_ThrowsKeyNotFoundException_WhenProjectNotFound(string name, string description, string link, string projectId)
        {
            // Arrange
            var mockProjectFacade = new Mock<IProjectFacade>();

            mockProjectFacade.Setup(x => x.FindProjectById(It.IsAny<int>()))
                             .ReturnsAsync((Core.Models.Project)null);

            var controller = new ProjectController(mockProjectFacade.Object);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => controller.Edit(name, description, link, projectId));
        }

        [Theory]
        [InlineData("Project 3", "Description 3", "url", "3")]
        [InlineData("Project 1", "Description 1", "url", "1")]
        [InlineData("Project 2", "Description 2", "", "2")]
        public async Task Edit_ReturnsJsonResult_WhenDatabaseUpdateSuccess(string name, string description, string link, string projectId)
        {
            // Arrange
            var mockProjectFacade = new Mock<IProjectFacade>();

            mockProjectFacade.Setup(x => x.FindProjectById(It.IsAny<int>()))
                             .ReturnsAsync(new Core.Models.Project());

            mockProjectFacade.Setup(x => x.UpdateProject(It.IsAny<Project>()))
                             .ReturnsAsync(1);

            var controller = new ProjectController(mockProjectFacade.Object);

            // Act
            var result = await controller.Edit(name, description, link, projectId);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task FindById_ThrowsArgumentException_WhenIdArgIsNullOrEmpty(string id)
        {
            // Arrange
            var mockProjectFacade = new Mock<IProjectFacade>();
            var controller = new ProjectController(mockProjectFacade.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => controller.FindById(id));
        }

        [Theory]
        [InlineData("1")]
        [InlineData("3")]
        public async Task FindById_ThrowsKeyNotFoundException_WhenProjectIsNotFound(string id)
        {
            // Arrange
            var mockProjectFacade = new Mock<IProjectFacade>();

            mockProjectFacade.Setup(x => x.FindProjectById(It.IsAny<int>()))
                             .ReturnsAsync((Core.Models.Project)null);

            var controller = new ProjectController(mockProjectFacade.Object);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => controller.FindById(id));
        }

        [Theory]
        [InlineData("1")]
        [InlineData("3")]
        public async Task FindById_ReturnsJsonResult_WhenProjectIsFound(string id)
        {
            // Arrange
            var mockProjectFacade = new Mock<IProjectFacade>();

            mockProjectFacade.Setup(x => x.FindProjectById(It.IsAny<int>()))
                             .ReturnsAsync(new Core.Models.Project());

            var controller = new ProjectController(mockProjectFacade.Object);

            // Act
            var result = await controller.FindById(id);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
        }
    }
}
