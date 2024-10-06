using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;
using Dorm.Domain.Responses;
using Dorm.Server.Contracts.Commands.Reservation.Create;
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
    public class CreateReservationCommandHandlerTests
    {
        private readonly Mock<IWasherService> _washerServiceMock;
        private readonly Mock<IOptions<AuthSettings>> _optionsMock;
        private readonly CreateReservationCommandHandler _handler;

        public CreateReservationCommandHandlerTests()
        {
            _washerServiceMock = new Mock<IWasherService>();
            _optionsMock = new Mock<IOptions<AuthSettings>>();
            _handler = new CreateReservationCommandHandler(_washerServiceMock.Object, _optionsMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsOccupiedResponse_WhenWasherIsOccupied()
        {
            // Arrange
            var reservationDto = new ReservationDto
            {
                WasherId = 1,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            };

            var command = new CreateReservationCommand(reservationDto, "testToken");

            _washerServiceMock.Setup(m => m.HasReservation(reservationDto.WasherId, reservationDto.StartTime, reservationDto.EndTime))
                .ReturnsAsync(new TestsResponse<bool>(true, null));

            _optionsMock.Setup(o => o.Value).Returns(new AuthSettings { SecretKey = "@#@#@#@#@##@@#@#@#%#$%#@%#@%#%#@%@#$@##@%#@%" });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result.Data);
            Assert.Equal("Washer is occupied. Try different time.", result.Description);
        }

        [Fact]
        public async Task Handle_CreatesReservation_WhenWasherIsAvailable()
        {
            // Arrange
            var reservationDto = new ReservationDto
            {
                WasherId = 1,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2) // EndTime
            };

            var command = new CreateReservationCommand(reservationDto, GenerateJwtToken(1));

            _washerServiceMock.Setup(m => m.HasReservation(reservationDto.WasherId, reservationDto.StartTime, reservationDto.EndTime))
                .ReturnsAsync(new TestsResponse<bool>(false, null)); 

            _washerServiceMock.Setup(m => m.Create(It.IsAny<ReservationDto>()))
                .ReturnsAsync(new TestsResponse<ReservationDto>(reservationDto, null));

            _optionsMock.Setup(o => o.Value).Returns(new AuthSettings { SecretKey = "@#@#@#@#@##@@#@#@#%#$%#@%#@%#%#@%@#$@##@%#@%" });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(reservationDto, result.Data);
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
