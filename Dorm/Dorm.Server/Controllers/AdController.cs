using Dorm.BLL.Interfaces;
using Dorm.Domain.Entities.Ad;
using Microsoft.AspNetCore.Mvc;

namespace Dorm.Server.Controllers
{
    [ApiController]
    [Route("api/ads")]
    public class AdController : ControllerBase
    {
        private readonly IAdService _adService;

        public AdController(IAdService adService)
        {
            _adService = adService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAd([FromBody] Ad model)
        {
            await _adService.CreateAd(model);
            return Ok();
        }
    }
}
