using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dorm.Server.Contracts.Commands.Ticket.Update
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, TestsResponse<TicketDto>>
    {
        private readonly ITicketService _ticketService;
        private readonly IOptions<AuthSettings> _options;
        public UpdateTicketCommandHandler(ITicketService ticketService, IOptions<AuthSettings> options)
        {
            _ticketService = ticketService;
            _options = options;
        }

        public async Task<TestsResponse<TicketDto>> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
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
            if(request.ticketDto.UserId == currentUserId)
                return await _ticketService.Update(request.ticketId, request.ticketDto);

            return new TestsResponse<TicketDto>(null, $"You cannot edit other users' tickets. Your ID is {currentUserId}");
        }
    }
}
