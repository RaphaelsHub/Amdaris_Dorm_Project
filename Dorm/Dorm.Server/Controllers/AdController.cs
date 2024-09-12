using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ad;
using Dorm.Server.Contracts.Commands.Ad.Create;
using Dorm.Server.Contracts.Queries.Ad.Get;
using Dorm.Server.Contracts.Queries.Ad.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Dorm.Server.Controllers
{
    [ApiController]
    [Route("api/ads")]
    public class AdController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAdService _adService;
        private readonly IOptions<AuthSettings> _authSettings;

        public AdController(IAdService adService, IMediator mediator, IOptions<AuthSettings> authSettings)
        {
            _mediator = mediator;
            _adService = adService;
            _authSettings = authSettings;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AdDto model)
        {
            var token = Request.Cookies["authToken"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            var response = await _mediator.Send(new CreateAdCommand(model, token));
            
            if (response == null)
            {
                return BadRequest(response.Description);
            }
            
            return Ok(response.Data);
        }

        [HttpGet("{adId}")]
        public async Task<IActionResult> Get([FromRoute] int adId)
        {
            var response = await _mediator.Send(new GetAdQuery(adId));
            
            if (response == null)
            {
                return BadRequest(response.Description);
            }

            return Ok(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllAdsQuery());
            
            if (response == null)
            {
                return BadRequest(response.Description);
            }

            return Ok(response.Data);
        }

        [HttpDelete("{adId}")]
        public async Task<IActionResult> Delete([FromRoute] int adId)
        {
            var response = await _adService.Delete(adId);
            
            if (response == null)
            {
                return BadRequest(response.Description);
            }

            return Ok();
        }

        [HttpPut("{adId}")]
        public async Task<IActionResult> Edit([FromRoute] int adId, [FromBody] AdDto model)
        {
            var response = await _adService.Edit(adId, model);
            if (response.Data == null)
            {
                return BadRequest(response.Description);
            }

            return Ok(response.Data);
        }
    }
}
