using System.Collections.Generic;
using System.Threading.Tasks;
using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using Moq;
using Xunit;

namespace Dorm.BLL.Tests
{
    public class ITicketServiceTests
    {
        private readonly Mock<ITicketService> _mockTicketService;

        public ITicketServiceTests()
        {
            _mockTicketService = new Mock<ITicketService>();
        }

        [Fact]
        public async Task GetById_ShouldReturnTicketDto_WhenTicketExists()
        {
            // Arrange
            var ticketId = 1;
            var ticketDto = new TicketDto { Id = ticketId, Subject = "Test Ticket Subject" };
            var expectedResponse = new BaseResponse<TicketDto>(ticketDto, null); 

            _mockTicketService.Setup(s => s.GetById(ticketId)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockTicketService.Object.GetById(ticketId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(ticketId, result.Data.Id);
            Assert.Equal("Test Ticket Subject", result.Data.Subject);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfTicketDtos()
        {
            // Arrange
            var ticketDtos = new List<TicketDto>
            {
                new TicketDto { Id = 1, Subject = "Test Ticket 1" },
                new TicketDto { Id = 2, Subject = "Test Ticket 2" }
            };
            var expectedResponse = new BaseResponse<IEnumerable<TicketDto>>(ticketDtos, null); // Передаем null для Description

            _mockTicketService.Setup(s => s.GetAll()).ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockTicketService.Object.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(2, ((List<TicketDto>)result.Data).Count);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedTicketDto()
        {
            // Arrange
            var ticketDto = new TicketDto { Subject = "New Ticket Subject" };
            var createdTicketDto = new TicketDto { Id = 3, Subject = "New Ticket Subject" };
            var expectedResponse = new BaseResponse<TicketDto>(createdTicketDto, null); 

            _mockTicketService.Setup(s => s.Create(ticketDto)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockTicketService.Object.Create(ticketDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(createdTicketDto.Subject, result.Data.Subject);
        }

        [Fact]
        public async Task Delete_ShouldReturnTrue_WhenTicketIsDeleted()
        {
            // Arrange
            var ticketId = 1;
            var expectedResponse = new BaseResponse<bool>(true, null); 

            _mockTicketService.Setup(s => s.Delete(ticketId)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockTicketService.Object.Delete(ticketId);

            // Assert
            Assert.True(result.Success);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task Update_ShouldReturnUpdatedTicketDto()
        {
            // Arrange
            var ticketId = 1;
            var ticketDto = new TicketDto { Subject = "Updated Ticket Subject" };
            var expectedResponse = new BaseResponse<TicketDto>(ticketDto, null); 

            _mockTicketService.Setup(s => s.Update(ticketId, ticketDto)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockTicketService.Object.Update(ticketId, ticketDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(ticketDto.Subject, result.Data.Subject);
        }

        [Fact]
        public async Task AddResponse_ShouldReturnUpdatedTicketDto()
        {
            // Arrange
            var ticketId = 1;
            var ticketDto = new TicketDto { Subject = "Ticket with Response" };
            var expectedResponse = new BaseResponse<TicketDto>(ticketDto, null); 

            _mockTicketService.Setup(s => s.AddResponse(ticketId, ticketDto)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockTicketService.Object.AddResponse(ticketId, ticketDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(ticketDto.Subject, result.Data.Subject);
        }
    }
}
