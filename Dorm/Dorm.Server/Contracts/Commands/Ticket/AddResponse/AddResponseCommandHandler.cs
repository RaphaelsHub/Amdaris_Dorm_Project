using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using Dorm.Server.Controllers;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dorm.Server.Contracts.Commands.Ticket.AddResponse
{
    public class AddResponseCommandHandler : IRequestHandler<AddResponseCommand, BaseResponse<TicketDto>>
    {
        private readonly ITicketService _ticketService;
        private readonly IOptions<AuthSettings> _options;
        private readonly IStudentProfileService _studentProfileService;
        public AddResponseCommandHandler(ITicketService ticketService, IOptions<AuthSettings> options, IStudentProfileService studentProfileService)
        {
            _ticketService = ticketService;
            _options = options;
            _studentProfileService = studentProfileService;
        }

        public async Task<BaseResponse<TicketDto>> Handle(AddResponseCommand request, CancellationToken cancellationToken)
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
            var currentUser = await _studentProfileService.GetById(currentUserId);
            request.ticketDto.RespondentId = currentUserId;
            request.ticketDto.RespondentName = currentUser.Data.FirstName + " " + currentUser.Data.LastName;
            request.ticketDto.RespondentEmail = currentUser.Data.Email;

            return await _ticketService.AddResponse(request.ticketId, request.ticketDto);
        }
    }
}
