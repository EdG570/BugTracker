using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using BugTracker.Core.Interfaces;
using System.Threading.Tasks;
using BugTracker.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Core.Services.UnitTests
{
    public class ProjectServiceTests
    {
        [Fact]
        public async Task Create_ShouldReturnInt_WhenNewProjectCreated()
        {
            // Arrange
            var project = new Project { Name = "Project 1", Description = "This is project one." };
            var mockProjectRepo = new Mock<IProjectRepository>();

            mockProjectRepo.Setup(x => x.Create(It.IsAny<Project>()))
                           .ReturnsAsync(1);

            var service = new ProjectService(mockProjectRepo.Object);

            // Act
            var result = await service.Create(project);

            // Assert
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnInt_WhenDeleteFinishes()
        {
            // Arrange
            var projectId = 1;
            var mockProjectRepo = new Mock<IProjectRepository>();

            mockProjectRepo.Setup(x => x.Delete(It.IsAny<int>()))
                           .ReturnsAsync(1);

            var service = new ProjectService(mockProjectRepo.Object);

            // Act
            var result = await service.Delete(projectId);

            // Assert
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task FindOne_ShouldReturnTypeProject_WhenInvoked()
        {
            // Arrange
            var project = new Project { Id = 1, Name = "Project 1", Description = "This is project one." };
            var projectId = 1;
            var mockProjectRepo = new Mock<IProjectRepository>();

            mockProjectRepo.Setup(x => x.FindOne(It.IsAny<int>()))
                           .ReturnsAsync(project);

            var service = new ProjectService(mockProjectRepo.Object);

            // Act
            var result = await service.FindOne(projectId);

            // Assert
            Assert.IsType<Project>(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnIEnumerableProject_WhenInvoked()
        {
            // Arrange
            var mockProjectRepo = new Mock<IProjectRepository>();

            mockProjectRepo.Setup(x => x.GetAll())
                           .ReturnsAsync(new List<Project>());

            var service = new ProjectService(mockProjectRepo.Object);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Project>>(result);
        }

        [Fact]
        public void GetSeason_ShouldReturnCurrentSeason_WhenInvoked()
        {
            // Arrange
            var month = DateTime.Now.Month;
            var spring = new List<int> { 3, 4, 5 };
            var summer = new List<int> { 6, 7, 8 };
            var fall = new List<int> { 9, 10, 11 };
            var winter = new List<int> { 1, 2, 12 };

            var mockProjectRepo = new Mock<IProjectRepository>();

            var service = new ProjectService(mockProjectRepo.Object);

            // Act
            var result = service.GetSeason();

            // Assert
            if (spring.Contains(month))
                Assert.Equal("spring-", result);
            else if (summer.Contains(month))
                Assert.Equal("summer-", result);
            else if (fall.Contains(month))
                Assert.Equal("fall-", result);
            else if (winter.Contains(month))
                Assert.Equal("winter-", result);
        }

        [Fact]
        public async Task GetUsersAsSelectListItemsByProjectId_ShouldReturnListOfSelectListItem_WhenProjectIsNull()
        {
            // Arrange
            var projectId = 2;
            var mockProjectRepo = new Mock<IProjectRepository>();

            mockProjectRepo.Setup(x => x.FindOne(projectId))
                           .ReturnsAsync((Project)null);

            var service = new ProjectService(mockProjectRepo.Object);

            // Act
            var result = await service.GetUsersAsSelectListItemsByProjectId(projectId);

            // Assert
            var viewResult = Assert.IsAssignableFrom<IEnumerable<SelectListItem>>(result);
        }

        [Fact]
        public async Task GetUsersAsSelectListItemsByProjectId_ShouldReturnListOfSelectListItem_WhenProjectFound()
        {
            // Arrange
            var projectId = 2;
            var mockProjectRepo = new Mock<IProjectRepository>();

            mockProjectRepo.Setup(x => x.FindOne(projectId))
                           .ReturnsAsync(new Project());

            var service = new ProjectService(mockProjectRepo.Object);

            // Act
            var result = await service.GetUsersAsSelectListItemsByProjectId(projectId);

            // Assert
            var viewResult = Assert.IsAssignableFrom<IEnumerable<SelectListItem>>(result);
        }

        [Fact]
        public async Task Update_ShouldReturnTypeInt_WhenUpdateFinishes()
        {
            // Assert
            var project = new Project { Id = 1, Name = "Project X", Description = "This is project x." };
            var mockProjectRepo = new Mock<IProjectRepository>();

            mockProjectRepo.Setup(x => x.Update(It.IsAny<Project>()))
                           .ReturnsAsync(1);

            var service = new ProjectService(mockProjectRepo.Object);

            // Act
            var result = await service.Update(project);

            // Assert
            Assert.IsType<int>(result);
        }
    }
}
