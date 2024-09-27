﻿using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dorm.Server.Contracts.Queries.Ticket.GetAll
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, TestsResponse<IEnumerable<TicketDto>>>
    {
        private readonly ITicketService _ticketService;
        private readonly IOptions<AuthSettings> _options;
        public GetAllTicketsQueryHandler(ITicketService ticketService, IOptions<AuthSettings> options)
        {
            _ticketService = ticketService;
            _options = options;
        }

        public async Task<TestsResponse<IEnumerable<TicketDto>>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
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
            var response =  await _ticketService.GetAll();

            foreach (var ticket in response.Data)
            {
                ticket.canEdit = ticket.UserId == currentUserId;
            }
            return response;
        }
    }
}
