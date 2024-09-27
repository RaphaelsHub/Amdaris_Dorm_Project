using Xunit;
using Moq;
using MediatR;
using Dorm.BLL.Interfaces;
using Dorm.Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Dorm.Domain.DTO;
using Dorm.Domain.Responses;
using Dorm.Server.Contracts.Queries.Ticket.Get;
using Dorm.Server.Contracts.Queries.Ticket.GetAll;
using Dorm.Server.Contracts.Commands.Ticket.Create;
using Dorm.Server.Contracts.Commands.Ticket.Delete;
using Dorm.Server.Contracts.Commands.Ticket.Update;
using Dorm.Server.Contracts.Commands.Ticket.AddResponse;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Dorm.Domain.Responces;

namespace Dorm.Tests
{
    public class TicketControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ITicketService> _ticketServiceMock;
        private readonly TicketController _ticketController;

        public TicketControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _ticketServiceMock = new Mock<ITicketService>();
            _ticketController = new TicketController(_mediatorMock.Object, _ticketServiceMock.Object);
        }

        private void SetUpHttpContextWithCookie(string token)
        {
            var cookieCollectionMock = new Mock<IRequestCookieCollection>();
            cookieCollectionMock.Setup(c => c["authToken"]).Returns(token);

            var requestMock = new Mock<HttpRequest>();
            requestMock.Setup(r => r.Cookies).Returns(cookieCollectionMock.Object);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(h => h.Request).Returns(requestMock.Object);

            _ticketController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };
        }

        [Fact]
        public async Task GetTicketById_ReturnsOk_WhenTicketExists()
        {
            // Arrange
            int ticketId = 1;
            string token = "some-token";
            var ticketData = new TicketDto { Id = ticketId };

            SetUpHttpContextWithCookie(token);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetTicketByIdQuery>(), default))
                .ReturnsAsync(new TestsResponse<TicketDto>(ticketData, "Success"));

            // Act
            var result = await _ticketController.GetTicketById(ticketId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(ticketData, okResult.Value);
        }

        [Fact]
        public async Task GetTicketById_ReturnsUnauthorized_WhenTokenIsMissing()
        {
            // Arrange
            int ticketId = 1;

            SetUpHttpContextWithCookie(null); 

            // Act
            var result = await _ticketController.GetTicketById(ticketId);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Token is missing", unauthorizedResult.Value);
        }

        [Fact]
        public async Task GetAllTickets_ReturnsOk_WhenTicketsExist()
        {
            // Arrange
            string token = "some-token";
            var ticketDataList = new List<TicketDto>
    {
        new TicketDto { Id = 1, UserId = 123, Name = "Test ticket", Subject = "Test subject" }
    };

            SetUpHttpContextWithCookie(token);
   
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllTicketsQuery>(), default))
                .ReturnsAsync(new TestsResponse<IEnumerable<TicketDto>>(ticketDataList, "Tickets retrieved successfully"));

            // Act
            var result = await _ticketController.GetAllTickets();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(ticketDataList, okResult.Value);
        }



        [Fact]
        public async Task CreateTicket_ReturnsCreatedAtAction_WhenTicketIsCreated()
        {
            // Arrange
            string token = "some-token";
            var ticketDto = new TicketDto
            {
                Id = 1,
                Subject = "Test ticket subject"  
            };

            SetUpHttpContextWithCookie(token);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateTicketCommand>(), default))
                .ReturnsAsync(new TestsResponse<TicketDto>(ticketDto, "Ticket created successfully"));

            // Act
            var result = await _ticketController.CreateTicket(ticketDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_ticketController.GetTicketById), createdAtActionResult.ActionName);
            Assert.Equal(ticketDto.Id, createdAtActionResult.RouteValues["ticketId"]);
            Assert.Equal(ticketDto, createdAtActionResult.Value);
        }

        [Fact]
        public async Task DeleteTicket_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            int ticketId = 1;

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteTicketCommand>(), default))
                .ReturnsAsync(new TestsResponse<bool>(true, "Ticket deleted successfully"));

            // Act
            var result = await _ticketController.DeleteTicket(ticketId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateTicket_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            int ticketId = 1;
            string token = "some-token";
            var ticketDto = new TicketDto
            {
                Id = ticketId,
                Subject = "Updated ticket subject"  
            };

            SetUpHttpContextWithCookie(token);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateTicketCommand>(), default))
                .ReturnsAsync(new TestsResponse<TicketDto>(ticketDto, "Ticket updated successfully"));

            // Act
            var result = await _ticketController.UpdateTicket(ticketId, ticketDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(ticketDto, okResult.Value);
        }


        [Fact]
        public async Task AddResponse_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            int ticketId = 1;
            string token = "some-token";
            var ticketDto = new TicketDto { Id = 1, Subject = "Some Subject" };

            SetUpHttpContextWithCookie(token);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AddResponseCommand>(), default))
                .ReturnsAsync(new TestsResponse<TicketDto>(ticketDto, "Response added successfully"));

            // Act
            var result = await _ticketController.AddResponse(ticketId, ticketDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(ticketDto, okResult.Value);
        }
    }
}
