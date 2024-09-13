using Dorm.Domain.Entities.Ad;
using Dorm.Domain.Entities.Ticket;
using Dorm.Domain.Enum.Ticket;
using Microsoft.EntityFrameworkCore;
using Dorm.Domain.Entities.UserEF;

namespace Dorm.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            InitializeDB(this);
        }
        public DbSet<UserEF> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Ad> Ads { get; set; }

        private static void InitializeDB(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new UserEF
                {
                    FirstName = "Vlad"
                });
                //context.SaveChanges();
            }
            if (!context.Tickets.Any())
            {
                context.Tickets.Add(new Ticket
                {
                    UserId = 1,
                    Name = "vlad",
                    Group = "2210",
                    Room = "23",
                    Type = TicketType.COMPLAINT,
                    Subject = "Tualet prorvalo",
                    Description = "Srat' net vozmojnosti(",
                    Status = TicketStatus.SENT,
                    Date = DateTime.UtcNow,
                });
            }
            context.SaveChanges();
        }
    }
}
