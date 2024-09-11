using Dorm.DAL;
using Dorm.Domain.Entities.Ticket;
using Dorm.Domain.Entities.User;
using Microsoft.AspNetCore.Mvc;

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
            _context.Users.Add(new User
            {
                FirstName = "Alex"
            });

            _context.Tickets.Add(new Ticket
            {
                Name = "vlad",
                Group = "2210",
                Room = "23",
                Type = Domain.Enums.TicketType.COMPLAINT,
                Subject = "Tualet prorvalo",
                Description = "Srat' net vozmojnosti(",
                Status = Domain.Enums.TicketStatus.SENT,
                Date = DateTime.UtcNow,
            });

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
