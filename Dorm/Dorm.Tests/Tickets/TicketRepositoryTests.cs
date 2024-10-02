using Dorm.DAL.Repositories;
using Dorm.Domain.Entities.Ticket;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dorm.DAL.Tests
{
    public class TicketRepositoryTests
    {
        private readonly TicketRepository _repository;
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public TicketRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TicketDatabase")
                .Options;

            var context = new ApplicationDbContext(_options);
            _repository = new TicketRepository(context);
        }

        [Fact]
        public async Task Create_ShouldAddTicket()
        {
            // Arrange
            var ticket = new Ticket { Id = 1, Subject = "Test Ticket", Description = "Test Description" };

            // Act
            var result = await _repository.Create(ticket);

            // Assert
            Assert.True(result);
            var createdTicket = await _repository.GetById(ticket.Id);
            Assert.NotNull(createdTicket);
            Assert.Equal(ticket.Subject, createdTicket.Subject);
        }

        [Fact]
        public async Task Delete_ShouldRemoveTicket()
        {
            // Arrange
            var ticket = new Ticket { Id = 2, Subject = "Test Ticket 2", Description = "Test Description 2" };
            await _repository.Create(ticket);

            // Act
            var result = await _repository.Delete(ticket);

            // Assert
            Assert.True(result);
            var deletedTicket = await _repository.GetById(ticket.Id);
            Assert.Null(deletedTicket);
        }

        [Fact]
        public async Task Update_ShouldModifyTicket()
        {
            // Arrange
            var ticket = new Ticket { Id = 3, Subject = "Old Subject", Description = "Old Description" };
            await _repository.Create(ticket);

            // Act
            ticket.Subject = "New Subject";
            var updatedTicket = await _repository.Update(ticket);

            // Assert
            Assert.Equal("New Subject", updatedTicket.Subject);
            var retrievedTicket = await _repository.GetById(ticket.Id);
            Assert.Equal("New Subject", retrievedTicket.Subject);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllTickets()
        {
            // Arrange
            var ticket1 = new Ticket { Id = 4, Subject = "Ticket 1", Description = "Description 1" };
            var ticket2 = new Ticket { Id = 5, Subject = "Ticket 2", Description = "Description 2" };
            await _repository.Create(ticket1);
            await _repository.Create(ticket2);

            // Act
            var tickets = await _repository.GetAll();

            // Assert
            Assert.Equal(2, tickets.Count());
        }

        [Fact]
        public async Task GetById_ShouldReturnTicket()
        {
            // Arrange
            var ticket = new Ticket { Id = 6, Subject = "Test Ticket 6", Description = "Test Description 6" };
            await _repository.Create(ticket);

            // Act
            var retrievedTicket = await _repository.GetById(ticket.Id);

            // Assert
            Assert.NotNull(retrievedTicket);
            Assert.Equal(ticket.Subject, retrievedTicket.Subject);
        }
    }
}
