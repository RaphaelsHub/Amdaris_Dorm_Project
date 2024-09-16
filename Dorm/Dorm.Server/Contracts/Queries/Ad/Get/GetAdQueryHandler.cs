using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dorm.Server.Contracts.Queries.Ad.Get
{
    public class GetAdQueryHandler
        : IRequestHandler<GetAdQuery, BaseResponse<AdDto>>
    {
        private readonly IAdService _adService;
        private readonly IOptions<AuthSettings> _options;

        public GetAdQueryHandler(IAdService adService, IOptions<AuthSettings> options)
        {
            _adService = adService;
            _options = options;
        }

        public async Task<BaseResponse<AdDto>> Handle(GetAdQuery request, CancellationToken cancellationToken)
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

            var response = await _adService.Get(request.adId);

            if (response.Data != null)
            {
                response.Data.CanEdit = response.Data.UserId == int.Parse(userIdClaim);

            }
            return response;


        }
    }
}
