using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using BugTracker.Core.Interfaces;
using AutoMapper;
using BugTracker.Application.Controllers;
using BugTracker.Core.Models;
using Microsoft.AspNetCore.Mvc;
using BugTracker.Application.ViewModels.ProjectViewModels;
using BugTracker.Application.ViewModels.TicketViewModels;
using System.Security.Claims;

namespace BugTracker.App.Controllers
{
    public class TicketTests
    {
        [Theory]
        [InlineData("", "open")]
        [InlineData("1", "")]
        [InlineData(null, "open")]
        [InlineData("1", null)]
        [InlineData("", "")]
        public async Task UpdateStatus_ThrowsArgumentException_WhenArgsAreNull(string id, string status)
        {
            // Arrange
            var mockTicketService = new Mock<ITicketService>();
            var mockProjectService = new Mock<IProjectService>();
            var mockMapper = new Mock<IMapper>();
            var mockAppUserService = new Mock<IAppUserService>();

            var controller = new TicketController(mockTicketService.Object, mockProjectService.Object, mockMapper.Object, mockAppUserService.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => controller.UpdateStatus(id, status));
        }

        [Theory]
        [InlineData("1", "open")]
        public async Task UpdateStatus_ReturnsJsonResult_WhenUpdateSuccess(string id, string status)
        {
            // Arrange
            var mockTicketService = new Mock<ITicketService>();
            var mockProjectService = new Mock<IProjectService>();
            var mockMapper = new Mock<IMapper>();
            var mockAppUserService = new Mock<IAppUserService>();

            mockTicketService.Setup(x => x.FindOne(It.IsAny<int>()))
                             .ReturnsAsync(new Core.Models.Ticket());

            mockTicketService.Setup(x => x.Update(It.IsAny<Ticket>()))
                             .ReturnsAsync(1);

            var controller = new TicketController(mockTicketService.Object, mockProjectService.Object, mockMapper.Object, mockAppUserService.Object);

            // Act
            var result = await controller.UpdateStatus(id, status);

            // Assert
            var viewResult = Assert.IsType<JsonResult>(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task FindById_ThrowsArgumentException_WhenArgIsNullOrEmpty(string id)
        {
            // Arrange
            var mockTicketService = new Mock<ITicketService>();
            var mockProjectService = new Mock<IProjectService>();
            var mockMapper = new Mock<IMapper>();
            var mockAppUserService = new Mock<IAppUserService>();

            var controller = new TicketController(mockTicketService.Object, mockProjectService.Object, mockMapper.Object, mockAppUserService.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => controller.FindById(id));
        }

        [Theory]
        [InlineData("1")]
        public async Task FindById_ReturnsJsonResult_TicketIsFoundById(string id)
        {
            // Arrange
            var mockTicketService = new Mock<ITicketService>();
            var mockProjectService = new Mock<IProjectService>();
            var mockMapper = new Mock<IMapper>();
            var mockAppUserService = new Mock<IAppUserService>();

            mockTicketService.Setup(x => x.FindOne(It.IsAny<int>()))
                             .ReturnsAsync(new Core.Models.Ticket());

            var controller = new TicketController(mockTicketService.Object, mockProjectService.Object, mockMapper.Object, mockAppUserService.Object);

            // Act 
            var result = await controller.FindById(id);

            // Assert
            var viewResult = Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsRedirectToActionResult_WhenPropsAreNullOrEmpty()
        {
            // Arrange
            var vm = new DetailViewModel
            {
                Project = new Core.Models.Project { Id =1 },
                TicketCreateVm = new TicketCreateViewModel
                {
                    Title = "",
                    Description = ""
                }
            };
            var mockTicketService = new Mock<ITicketService>();
            var mockProjectService = new Mock<IProjectService>();
            var mockMapper = new Mock<IMapper>();
            var mockAppUserService = new Mock<IAppUserService>();

            var controller = new TicketController(mockTicketService.Object, mockProjectService.Object, mockMapper.Object, mockAppUserService.Object);

            // Act
            var result = await controller.Create(vm);

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsRedirectToActionResult_WhenCreateSuccess()
        {
            // Arrange
            var vm = new DetailViewModel
            {
                Project = new Core.Models.Project { Id = 1 },
                TicketCreateVm = new TicketCreateViewModel
                {
                    Title = "This is a title",
                    Description = "This is a description"
                }
            };
            var mockTicketService = new Mock<ITicketService>();
            var mockProjectService = new Mock<IProjectService>();
            var mockMapper = new Mock<IMapper>();
            var mockAppUserService = new Mock<IAppUserService>();

            mockAppUserService.Setup(x => x.GetUserByClaim(It.IsAny<ClaimsPrincipal>()))
                              .ReturnsAsync(new Core.Models.AppUser());

            mockMapper.Setup(x => x.Map(It.IsAny<TicketCreateViewModel>(), It.IsAny<Core.Models.Ticket>()))
                      .Returns(new Core.Models.Ticket());

            mockTicketService.Setup(x => x.Create(It.IsAny<Ticket>()))
                             .ReturnsAsync(1);

            var controller = new TicketController(mockTicketService.Object, mockProjectService.Object, mockMapper.Object, mockAppUserService.Object);

            // Act
            var result = await controller.Create(vm);

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
