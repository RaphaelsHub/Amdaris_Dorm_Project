using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dorm.Server.Contracts.Commands.Ad.Create
{
    public class CreateAdCommandHandler
        : IRequestHandler<CreateAdCommand, BaseResponse<AdDto>>
    {
        private readonly IAdService _adService;
        private readonly IOptions<AuthSettings> _options;

        public CreateAdCommandHandler(IAdService adService, IOptions<AuthSettings> options)
        {
            _adService = adService;
            _options = options;
        }

        public async Task<BaseResponse<AdDto>> Handle(CreateAdCommand request, CancellationToken cancellationToken)
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

            // обновляем значение userId
            var updatedModel = request.model with { UserId = int.Parse(userIdClaim) };

            var response = await _adService.Create(updatedModel);

            return response;
        }
    }
}
