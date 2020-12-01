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
    public class TicketServiceTests
    {
        [Fact]
        public async Task Create_ShouldReturnInt_WhenCreateFinishes()
        {
            // Arrange
            var ticket = new Ticket { Title = "Ticket 1", Description = "This is ticket one." };
            var mockTicketRepo = new Mock<ITicketRepository>();

            mockTicketRepo.Setup(x => x.Create(ticket))
                          .ReturnsAsync(1);

            var service = new TicketService(mockTicketRepo.Object);

            // Act
            var result = await service.Create(ticket);

            // Assert
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnInt_WhenCompleted()
        {
            // Arrange
            var ticketId = 1;
            var mockTicketRepo = new Mock<ITicketRepository>();

            mockTicketRepo.Setup(x => x.Delete(ticketId))
                          .ReturnsAsync(1);

            var service = new TicketService(mockTicketRepo.Object);

            // Act
            var result = await service.Delete(ticketId);

            // Assert
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task FindOne_ShouldReturnTicket_WhenIdIsFound()
        {
            // Arrange
            var ticket = new Ticket { Title = "Ticket 1", Description = "This is ticket one." };
            var ticketId = 1;
            var mockTicketRepo = new Mock<ITicketRepository>();

            mockTicketRepo.Setup(x => x.FindOne(ticketId))
                          .ReturnsAsync(ticket);

            var service = new TicketService(mockTicketRepo.Object);

            // Act
            var result = await service.FindOne(ticketId);

            // Assert
            Assert.IsType<Ticket>(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnIEnumerableTicket_WhenInvoked()
        {
            // Arrange
            var mockTicketRepo = new Mock<ITicketRepository>();

            mockTicketRepo.Setup(x => x.GetAll())
                          .ReturnsAsync(new List<Ticket>());

            var service = new TicketService(mockTicketRepo.Object);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Ticket>>(result);
        }

        [Fact]
        public async Task GetAllTicketsByUserId_ShouldReturnIEnumerableTicket_WhenInvoked()
        {
            // Arrange
            var userId = 1;
            var tickets = new List<Ticket> 
            {
                new Ticket { Id = 1, AssignedToId = 1, RequestorId = 6 },
                new Ticket { Id = 2, AssignedToId = 3, RequestorId = 6 },
                new Ticket { Id = 3, AssignedToId = 4, RequestorId = 6 },
                new Ticket { Id = 4, AssignedToId = 2, RequestorId = 6 },
                new Ticket { Id = 5, AssignedToId = 7, RequestorId = 1 }
            };

            var mockTicketRepo = new Mock<ITicketRepository>();

            mockTicketRepo.Setup(x => x.GetAll())
                          .ReturnsAsync(tickets);

            var service = new TicketService(mockTicketRepo.Object);

            // Act
            var result = await service.GetAllTicketsByUserId(userId);

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Ticket>>(result);
        }

        [Fact]
        public async Task Update_ShouldReturnInt_WhenUpdateCompletes()
        {
            // Arrange
            var ticket = new Ticket { Id = 1, Title = "Ticket", Description = "This is a ticket." };
            var mockTicketRepo = new Mock<ITicketRepository>();

            mockTicketRepo.Setup(x => x.Update(It.IsAny<Ticket>()))
                          .ReturnsAsync(1);

            var service = new TicketService(mockTicketRepo.Object);

            // Act
            var result = await service.Update(ticket);

            // Assert
            Assert.IsType<int>(result);
        }
    }
}
