using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;
using Dorm.Domain.Responses;
using Dorm.Server.Contracts.Commands.Reservation.Create;
using Dorm.Server.Contracts.Commands.Reservation.Delete;
using Dorm.Server.Contracts.Queries.Reservation.GetAll;
using Dorm.Server.Contracts.Queries.Reservation.GetAllByUserId;
using Dorm.Server.Contracts.Queries.Reservation.GetAllByWasherId;
using Dorm.Server.Contracts.Queries.Reservation.GetById;
using Dorm.Server.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Dorm.Server.Tests.Controllers
{
    public class ReservationControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ReservationController _controller;

        public ReservationControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ReservationController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenReservationExists()
        {
            // Arrange
            var reservationId = 1;
            var reservationData = new ReservationDto
            {
                WasherId = 3,
                UserId = 2,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            };
            var response = new TestsResponse<ReservationDto>(reservationData, null);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(response);

            // Act
            var result = await _controller.GetById(reservationId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(reservationData, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenReservationDoesNotExist()
        {
            // Arrange
            var reservationId = 1;
            var response = new TestsResponse<ReservationDto>(null, "Not found");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(response);

            // Act
            var result = await _controller.GetById(reservationId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Not found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAllReservations_ReturnsOk_WhenReservationsExist()
        {
            // Arrange
            var reservationData = new List<ReservationDto>
            {
                new ReservationDto
                {
                    WasherId = 3,
                    UserId = 2,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(1)
                },
                new ReservationDto
                {
                    WasherId = 5,
                    UserId = 4,
                    StartTime = DateTime.Now.AddHours(1),
                    EndTime = DateTime.Now.AddHours(2)
                }
            };
            var response = new BaseResponse<IEnumerable<ReservationDto>>(reservationData, null);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllReservationsQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllReservations();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(reservationData, okResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsOk_WhenCreationIsSuccessful()
        {
            // Arrange
            var reservationDto = new ReservationDto
            {
                WasherId = 3,
                UserId = 2,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            };
            var token = "testToken";
            var response = new TestsResponse<ReservationDto>(reservationDto, null);

            // Установка контекста HTTP
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = $"authToken={token}";
            _controller.ControllerContext.HttpContext = httpContext;

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateReservationCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(response);

            // Act
            var result = await _controller.Create(reservationDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(reservationDto, okResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsUnauthorized_WhenTokenIsMissing()
        {
            // Arrange
            var reservationDto = new ReservationDto();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await _controller.Create(reservationDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Token is missing", unauthorizedResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            var reservationId = 1;
            var token = "testToken";
            var response = new TestsResponse<bool>(true, null);

            // Установка контекста HTTP
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = $"authToken={token}";
            _controller.ControllerContext.HttpContext = httpContext;

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteReservationCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(response);

            // Act
            var result = await _controller.Delete(reservationId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenReservationDoesNotExist()
        {
            // Arrange
            var reservationId = 1;
            var token = "testToken";
            var response = new TestsResponse<bool>(false, "Not found");

            // Установка контекста HTTP
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = $"authToken={token}";
            _controller.ControllerContext.HttpContext = httpContext;

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteReservationCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(response);

            // Act
            var result = await _controller.Delete(reservationId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Not found", notFoundResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsUnauthorized_WhenTokenIsMissing()
        {
            // Arrange
            var reservationId = 1;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await _controller.Delete(reservationId);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Token is missing", unauthorizedResult.Value);
        }

        [Fact]
        public async Task GetAllByWasherId_ReturnsOk_WhenReservationsExist()
        {
            // Arrange
            var washerId = 1;
            var reservationData = new List<ReservationDto>
            {
                new ReservationDto
                {
                    WasherId = washerId,
                    UserId = 2,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(1)
                }
            };
            var response = new BaseResponse<IEnumerable<ReservationDto>>(reservationData, null);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllByWasherIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllByWasherId(washerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(reservationData, okResult.Value);
        }

        [Fact]
        public async Task GetAllByWasherId_ReturnsNotFound_WhenNoReservationsExist()
        {
            // Arrange
            var washerId = 1;
            var response = new BaseResponse<IEnumerable<ReservationDto>>(null, "No reservations found");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllByWasherIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllByWasherId(washerId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No reservations found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAllByUserId_ReturnsOk_WhenReservationsExist()
        {
            // Arrange
            var userId = 1;
            var reservationData = new List<ReservationDto>
            {
                new ReservationDto
                {
                    WasherId = 3,
                    UserId = userId,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(1)
                }
            };
            var response = new BaseResponse<IEnumerable<ReservationDto>>(reservationData, null);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllByUserIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllByUserId(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(reservationData, okResult.Value);
        }

        [Fact]
        public async Task GetAllByUserId_ReturnsNotFound_WhenNoReservationsExist()
        {
            // Arrange
            var userId = 1;
            var response = new BaseResponse<IEnumerable<ReservationDto>>(null, "No reservations found");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllByUserIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllByUserId(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No reservations found", notFoundResult.Value);
        }
    }
}

