using Dorm.DAL;
using Microsoft.AspNetCore.Mvc;
using Dorm.Domain.Entities.UserEF;

namespace Dorm.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApplicationDbContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _context.Reservations.FirstOrDefault(x => x.Id == 1);
            _context.Users.Add(new UserEF
            {
                FirstName = "Alex"
            });

            /*_context.Reservations.Add(new Domain.Entities.Laundry.Reservation
            {
                WasherId = 1,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddMinutes(120),
                UserId = 2
            });*/

            _context.SaveChanges();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
