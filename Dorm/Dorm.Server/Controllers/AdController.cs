using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ad;
using Dorm.Server.Contracts.Commands.Ad.Create;
using Dorm.Server.Contracts.Commands.Ad.Delete;
using Dorm.Server.Contracts.Commands.Ad.Edit;
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

        public AdController(IMediator mediator)
        {
            _mediator = mediator;
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
            
            if (response.Data == null)
            {
                return BadRequest(response.Description);
            }
            
            return Ok(response.Data);
        }

        [HttpGet("{adId}")]
        public async Task<IActionResult> Get([FromRoute] int adId)
        {
            var token = Request.Cookies["authToken"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            var response = await _mediator.Send(new GetAdQuery(adId, token));
            

            if (response.Data == null) 
            {
                return BadRequest(response.Description);
            }

            return Ok(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllAdsQuery());
            
            if (response.Data == null)
            {
                return BadRequest(response.Description);
            }

            return Ok(response.Data);
        }

        [HttpDelete("{adId}")]
        public async Task<IActionResult> Delete([FromRoute] int adId)
        {
            var response = await _mediator.Send(new DeleteAdCommand(adId));
            
            if (response.Data == false)
            {
                return BadRequest(response.Description);
            }

            return Ok(response.Data);
        }

        [HttpPut("{adId}")]
        public async Task<IActionResult> Edit([FromRoute] int adId, [FromBody] AdDto model)
        {
            var response = await _mediator.Send(new EditAdCommand(adId, model));

            if (response.Data == null)
            {
                return BadRequest(response.Description);
            }

            return Ok(response.Data);
        }
    }
}
