using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dorm.Server.Contracts.Commands.Ad.Edit
{
    public class EditAdCommandHandler
        : IRequestHandler<EditAdCommand, TestsResponse<AdDto>>
    {
        private readonly IAdService _adService;
        private readonly IOptions<AuthSettings> _options;

        public EditAdCommandHandler(IAdService adService, IOptions<AuthSettings> options)
        {
            _adService = adService;
            _options = options;
        }

        public async Task<TestsResponse<AdDto>> Handle(EditAdCommand request, CancellationToken cancellationToken)
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

            request.model.UserId = int.Parse(userIdClaim);

            return await _adService.Edit(request.adId, request.model);
        }
    }
}
