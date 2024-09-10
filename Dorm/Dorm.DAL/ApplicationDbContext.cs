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
    
        private static void InitializeDB(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Name = "Vlad"
                });
                context.SaveChanges();
            }
        }
    }
}
