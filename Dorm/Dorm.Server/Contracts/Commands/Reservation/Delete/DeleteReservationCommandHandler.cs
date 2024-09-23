using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.Responces;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dorm.Server.Contracts.Commands.Reservation.Delete
{
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, BaseResponse<bool>>
    {
        private readonly IWasherService _washerService;
        private readonly IOptions<AuthSettings> _options;
        public DeleteReservationCommandHandler(IWasherService washerService, IOptions<AuthSettings> options)
        {
            _washerService = washerService;
            _options = options;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.Value.SecretKey!);

            var principal = tokenHandler.ValidateToken(request.token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            var currentUserId = int.Parse(principal.FindFirst("id")?.Value);
            var reservation = await _washerService.GetById(request.reservationId);
            if(reservation.Data.UserId == currentUserId)
                return await _washerService.Delete(request.reservationId);

            return new BaseResponse<bool>(false, $"You cannot delete other users' reservations. Your ID is {currentUserId}");
        }
    }
}
