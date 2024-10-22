using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Responces;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dorm.Server.Contracts.Commands.Reservation.Create
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, BaseResponse<ReservationDto>>
    {
        private readonly IWasherService _washerService;
        private readonly IOptions<AuthSettings> _options;

        public CreateReservationCommandHandler(IWasherService washerService, IOptions<AuthSettings> options)
        {
            _washerService = washerService;
            _options = options;
        }

        public async Task<BaseResponse<ReservationDto>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservations = await _washerService.HasReservation(request.reservationDto.WasherId, request.reservationDto.StartTime, request.reservationDto.EndTime);
            if (reservations.Data)
            {
                return new BaseResponse<ReservationDto>(null, "Washer is occupied. Try different time.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.Value.SecretKey!);

            var principal = tokenHandler.ValidateToken(request.token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            request.reservationDto.UserId = int.Parse(principal.FindFirst("id")?.Value);
            // request.reservationDto.EndTime = request.reservationDto.StartTime.AddHours(2);

            return await _washerService.Create(request.reservationDto);
        }
    }
}
