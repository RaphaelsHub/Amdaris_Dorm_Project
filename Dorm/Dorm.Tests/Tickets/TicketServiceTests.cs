using AutoMapper;
using Dorm.BLL.Interfaces;
using Dorm.BLL.Services;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ticket;
using Dorm.Domain.Responces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace Dorm.BLL.Tests
{
    public class TicketServiceTests
    {
        private readonly Mock<ITicketRepository> _ticketRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TicketService _ticketService;

        public TicketServiceTests()
        {
            _ticketRepositoryMock = new Mock<ITicketRepository>();
            _mapperMock = new Mock<IMapper>();
            _ticketService = new TicketService(_ticketRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddResponse_TicketExists_ReturnsSuccess()
        {
            // Arrange
            var ticketId = 1;
            var ticketDto = new TicketDto
            {
                RespondentId = 2,
                RespondentName = "John Doe",
                RespondentEmail = "john@example.com",
                Response = "Test response"
            };
            var ticket = new Ticket { Id = ticketId };

            _ticketRepositoryMock.Setup(repo => repo.GetById(ticketId)).ReturnsAsync(ticket);
            _mapperMock.Setup(m => m.Map<TicketDto>(It.IsAny<Ticket>())).Returns(ticketDto);

            // Act
            var result = await _ticketService.AddResponse(ticketId, ticketDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Success.", result.Description);
            _ticketRepositoryMock.Verify(repo => repo.Update(It.IsAny<Ticket>()), Times.Once);
        }

        [Fact]
        public async Task AddResponse_TicketNotFound_ReturnsError()
        {
            // Arrange
            var ticketId = 1;
            var ticketDto = new TicketDto();

            _ticketRepositoryMock.Setup(repo => repo.GetById(ticketId)).ReturnsAsync((Ticket)null);

            // Act
            var result = await _ticketService.AddResponse(ticketId, ticketDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal($"Ticket with ID {ticketId} not found.", result.Description);
        }

        [Fact]
        public async Task Create_ValidTicket_ReturnsSuccess()
        {
            // Arrange
            var ticketDto = new TicketDto
            {
                Id = 1,
                UserId = 23,
                Name = " Jane Doe",
                RespondentName = "Jan Doe",
                RespondentEmail = "jane@example.com",
                Response = "New ticket"
            };
            var ticket = new Ticket
            {
                Id = 1,
                UserId = 23,
                Name = " Ja e",
                RespondentName = "Ja e",
                RespondentEmail = "jae@example.com",
                Response = "New ticket"
            };

            _mapperMock.Setup(m => m.Map<Ticket>(ticketDto)).Returns(ticket);
            _ticketRepositoryMock.Setup(repo => repo.Create(ticket)).ReturnsAsync(true); 
            _mapperMock.Setup(m => m.Map<TicketDto>(ticket)).Returns(ticketDto);

            // Act
            var result = await _ticketService.Create(ticketDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Success.", result.Description);
        }

        [Fact]
        public async Task Delete_TicketExists_ReturnsSuccess()
        {
            // Arrange
            var ticketId = 1;
            var ticket = new Ticket { Id = ticketId };

            _ticketRepositoryMock.Setup(repo => repo.GetById(ticketId)).ReturnsAsync(ticket);
            _ticketRepositoryMock.Setup(repo => repo.Delete(ticket)).ReturnsAsync(true);

            // Act
            var result = await _ticketService.Delete(ticketId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Success.", result.Description);
        }

        [Fact]
        public async Task Delete_TicketNotFound_ReturnsError()
        {
            // Arrange
            var ticketId = 1232;

            // Настройка мока
            _ticketRepositoryMock.Setup(repo => repo.GetById(ticketId)).ReturnsAsync((Ticket)null);

            // Act
            var result = await _ticketService.Delete(ticketId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Data);  
            Assert.Equal($"Ticket with ID {ticketId} not found.", result.Description);
        }

        [Fact]
        public async Task GetAll_ReturnsTickets()
        {
            // Arrange
            var tickets = new List<Ticket>
            {
                new Ticket { Id = 1 },
                new Ticket { Id = 2 }
            };

            _ticketRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(tickets);
            _mapperMock.Setup(m => m.Map<IEnumerable<TicketDto>>(tickets)).Returns(new List<TicketDto> { new TicketDto { Id = 1 }, new TicketDto { Id = 2 } });

            // Act
            var result = await _ticketService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Success.", result.Description);
        }

        [Fact]
        public async Task GetById_TicketExists_ReturnsTicket()
        {
            // Arrange
            var ticketId = 1;
            var ticket = new Ticket { Id = ticketId };
            var ticketDto = new TicketDto { Id = ticketId };

            _ticketRepositoryMock.Setup(repo => repo.GetById(ticketId)).ReturnsAsync(ticket);
            _mapperMock.Setup(m => m.Map<TicketDto>(ticket)).Returns(ticketDto);

            // Act
            var result = await _ticketService.GetById(ticketId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Success.", result.Description);
            Assert.Equal(ticketDto.Id, result.Data?.Id); 
        }

        [Fact]
        public async Task Update_ValidTicket_ReturnsUpdatedTicket()
        {
            // Arrange
            var ticketId = 1;
            var ticketDto = new TicketDto
            {
                RespondentId = 1,
                RespondentName = "Jane Doe",
                RespondentEmail = "jane@example.com",
                Response = "Updated ticket"
            };

            var existingTicket = new Ticket
            {
                Id = ticketId,
                RespondentId = 1,
                RespondentName = "Old Name",
                RespondentEmail = "old@example.com",
                Response = "Old response"
            };

            // Настройка моков
            _ticketRepositoryMock.Setup(repo => repo.GetById(ticketId)).ReturnsAsync(existingTicket); 
            _mapperMock.Setup(m => m.Map(ticketDto, existingTicket)); 
            _ticketRepositoryMock.Setup(repo => repo.Update(existingTicket)).ReturnsAsync(existingTicket);
            _mapperMock.Setup(m => m.Map<TicketDto>(existingTicket)).Returns(ticketDto);

            // Act
            var result = await _ticketService.Update(ticketId, ticketDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success, "Expected result to be successful.");
            Assert.Equal("Success.", result.Description);
            Assert.Equal(ticketDto.RespondentName, result.Data?.RespondentName); 
        }



        [Fact]
        public async Task Update_TicketNotFound_ReturnsError()
        {
            // Arrange
            var ticketId = 1;
            var ticketDto = new TicketDto();

            _ticketRepositoryMock.Setup(repo => repo.GetById(ticketId)).ReturnsAsync((Ticket)null);

            // Act
            var result = await _ticketService.Update(ticketId, ticketDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal($"Ticket with ID {ticketId} not found.", result.Description);
        }
    }
}
