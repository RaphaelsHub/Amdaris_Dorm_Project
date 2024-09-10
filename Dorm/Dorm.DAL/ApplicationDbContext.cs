using Dorm.Domain.Entities.Ticket;
using Dorm.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            InitializeDB(this);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    
        private static void InitializeDB(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Name = "Vlad"
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
                    Type = Domain.Enum.TicketType.COMPLAINT,
                    Subject = "Tualet prorvalo",
                    Description = "Srat' net vozmojnosti(",
                    Status = Domain.Enum.TicketStatus.SENT,
                    Date = DateTime.UtcNow,
                });
            }
            context.SaveChanges();
        }
    }
}
