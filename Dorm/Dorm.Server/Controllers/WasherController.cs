using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dorm.Server.Controllers
{
    [ApiController]
    [Route("api/washers")]
    public class WasherController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WasherController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("{reservationId}")] // Под вопросом
        public async Task<IActionResult> Reserve([FromRoute] int reservationId, [FromBody] DateTime startTime)
        {

            return Ok();
        }
    }
}
