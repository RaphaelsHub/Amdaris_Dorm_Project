using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.Server.Contracts.Queries.Ticket.Get
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, BaseResponse<TicketDto>>
    {
        private readonly ITicketService _ticketService;
        private readonly IOptions<AuthSettings> _options;

        public GetTicketByIdQueryHandler(ITicketService ticketService, IOptions<AuthSettings> options)
        {
            _ticketService = ticketService;
            _options = options;
        }
        public async Task<BaseResponse<TicketDto>> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.Value.SecretKey);

            var principal = tokenHandler.ValidateToken(request.token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            var userIdClaim = principal.FindFirst("id")?.Value;

            var response = await _ticketService.GetById(request.ticketId);
            if(response.Data != null) 
            {
                response.Data.canEdit = response.Data.UserId == int.Parse(userIdClaim);
            }
            return response;
        }
    }
}
