using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dorm.Server.Contracts.Commands.Ticket.Create
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, BaseResponse<TicketDto>>
    {
        private readonly ITicketService _ticketService;
        private readonly IOptions<AuthSettings> _options;
        public CreateTicketCommandHandler(ITicketService ticketService, IOptions<AuthSettings> options)
        {
            _ticketService = ticketService;
            _options = options;
        }

        public async Task<BaseResponse<TicketDto>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
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

            var userIdClaim = principal.FindFirst("id")?.Value;
            request.ticketDto.UserId = int.Parse(userIdClaim);
            return await _ticketService.Create(request.ticketDto);
        }
    }
}
