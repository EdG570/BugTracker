using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;

namespace BugTracker.Core.Services.UnitTests
{
    public class NotificationServiceTests
    {
        [Fact]
        public async Task Create_ReturnsInt_WhenCreateCompletes()
        {
            // Arrange
            var notification = new Notification { Message = "This is a message" };
            var mockNotificationRepo = new Mock<INotificationRepository>();

            mockNotificationRepo.Setup(x => x.Create(notification))
                                .ReturnsAsync(1);

            var service = new NotificationService(mockNotificationRepo.Object);

            // Act
            var result = await service.Create(notification);

            // Assert
            Assert.IsAssignableFrom<int>(result);
        }

        [Fact]
        public async Task Delete_ReturnsInt_OnCompletion()
        {
            // Arrange
            var id = 1;
            var mockNotificationRepo = new Mock<INotificationRepository>();

            mockNotificationRepo.Setup(x => x.Delete(id))
                                .ReturnsAsync(1);

            var service = new NotificationService(mockNotificationRepo.Object);

            // Act
            var result = await service.Delete(id);

            // Assert
            Assert.IsAssignableFrom<int>(result);
        }

        [Fact]
        public async Task FindOne_ReturnsNotification_WhenSuccess()
        {
            // Arrange
            var id = 1;
            var mockNotificationRepo = new Mock<INotificationRepository>();

            mockNotificationRepo.Setup(x => x.FindOne(id))
                                .ReturnsAsync(new Notification());

            var service = new NotificationService(mockNotificationRepo.Object);

            // Act
            var result = await service.FindOne(id);

            // Assert
            Assert.IsAssignableFrom<Notification>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsIEnumerableOfNotification_WhenInvoked()
        {
            // Arrange
            var notifications = new List<Notification>();
            var mockNotificationRepo = new Mock<INotificationRepository>();

            mockNotificationRepo.Setup(x => x.GetAll())
                                .ReturnsAsync(notifications);

            var service = new NotificationService(mockNotificationRepo.Object);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Notification>>(result);
        }

        [Fact]
        public async Task GetAllByUserId_ReturnsNotifications_WhenUserIdFoundAndIsAcknowlegedIsFalse()
        {
            // Arrange
            var userId = 1;
            var notifications = new List<Notification>
            {
                new Notification { Id = 1, AppUser = new AppUser { Id = 1 }, IsAcknowleged = false },
                new Notification { Id = 2, AppUser = new AppUser { Id = 6 }, IsAcknowleged = false },
                new Notification { Id = 3, AppUser = new AppUser { Id = 4 }, IsAcknowleged = false },
                new Notification { Id = 4, AppUser = new AppUser { Id = 1 }, IsAcknowleged = true }
            };

            var mockNotificationRepo = new Mock<INotificationRepository>();

            mockNotificationRepo.Setup(x => x.GetAll())
                                .ReturnsAsync(notifications);

            var service = new NotificationService(mockNotificationRepo.Object);

            // Act
            var result = await service.GetAllByUserId(userId);

            // Assert
            var viewResult = Assert.IsAssignableFrom<IEnumerable<Notification>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Update_ReturnsInt_WhenUpdateCompletes()
        {
            // Arrange
            var notification = new Notification { Id = 1, Message = "This is a message." };
            var mockNotificationRepo = new Mock<INotificationRepository>();

            mockNotificationRepo.Setup(x => x.Update(notification))
                                .ReturnsAsync(1);

            var service = new NotificationService(mockNotificationRepo.Object);

            // Act
            var result = await service.Update(notification);

            // Assert
            Assert.IsAssignableFrom<int>(result);
        }
    }
}
