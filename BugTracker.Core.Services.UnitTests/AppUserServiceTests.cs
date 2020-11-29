using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;

namespace BugTracker.Core.Services.UnitTests
{
    public class AppUserServiceTests
    {
        [Fact]
        public async Task FindOne_ReturnsAppUser_WhenIdArgIsValid()
        {
            // Arrange
            var idArg = 1;
            var mockAppUserRepo = new Mock<IAppUserRepository>();

            mockAppUserRepo.Setup(x => x.FindOne(idArg))
                           .ReturnsAsync(new AppUser());

            var service = new AppUserService(mockAppUserRepo.Object);

            // Act
            var result = await service.FindOne(idArg);

            // Assert
            Assert.IsType<AppUser>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsIEnumerableOfAppUser_WhenInvoked()
        {
            // Arrange
            var mockAppUserRepo = new Mock<IAppUserRepository>();

            mockAppUserRepo.Setup(x => x.GetAll())
                           .ReturnsAsync(new List<AppUser>());

            var service = new AppUserService(mockAppUserRepo.Object);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<AppUser>>(result);
        }
    }
}
