using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;
using Dorm.Domain.Responses;
using Dorm.Server.Contracts.Commands.Reservation.Delete;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Dorm.Server.Tests.Handlers
{
    public class DeleteReservationCommandHandlerTests
    {
        private readonly Mock<IWasherService> _washerServiceMock;
        private readonly Mock<IOptions<AuthSettings>> _optionsMock;
        private readonly DeleteReservationCommandHandler _handler;

        public DeleteReservationCommandHandlerTests()
        {
            _washerServiceMock = new Mock<IWasherService>();
            _optionsMock = new Mock<IOptions<AuthSettings>>();
            _handler = new DeleteReservationCommandHandler(_washerServiceMock.Object, _optionsMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenUserIsOwner()
        {
            // Arrange
            var reservationId = 1;
            var token = GenerateJwtToken(1); 
            var reservation = new TestsResponse<ReservationDto>(new ReservationDto
            {
                UserId = 1 
            }, null);

            _washerServiceMock.Setup(m => m.GetById(reservationId)).ReturnsAsync(reservation);
            _washerServiceMock.Setup(m => m.Delete(reservationId)).ReturnsAsync(new TestsResponse<bool>(true, null));

            _optionsMock.Setup(o => o.Value).Returns(new AuthSettings { SecretKey = "@#@#@#@#@##@@#@#@#%#$%#@%#@%#%#@%@#$@##@%#@%" });

            var command = new DeleteReservationCommand(reservationId, token);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Data);
            Assert.Null(result.Description);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenUserIsNotOwner()
        {
            // Arrange
            var reservationId = 1;
            var token = GenerateJwtToken(2);
            var reservation = new TestsResponse<ReservationDto>(new ReservationDto
            {
                UserId = 1
            }, null);

            _washerServiceMock.Setup(m => m.GetById(reservationId)).ReturnsAsync(reservation);

            _optionsMock.Setup(o => o.Value).Returns(new AuthSettings { SecretKey = "@#@#@#@#@##@@#@#@#%#$%#@%#@%#%#@%@#$@##@%#@%" });

            var command = new DeleteReservationCommand(reservationId, token);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Data);
            Assert.Equal("You cannot delete other users' reservations. Your ID is 2", result.Description);
        }

        private string GenerateJwtToken(int userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@#@#@#@#@##@@#@#@#%#$%#@%#@%#%#@%@#$@##@%#@%"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", userId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
