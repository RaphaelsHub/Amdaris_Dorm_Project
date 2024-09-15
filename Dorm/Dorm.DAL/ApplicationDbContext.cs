using Dorm.Domain.Entities.Ad;
using Dorm.Domain.Entities.Ticket;
using Dorm.Domain.Enum.Ticket;
using Microsoft.EntityFrameworkCore;
using Dorm.Domain.Entities.UserEF;
using Dorm.Domain.Entities.Laundry;

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
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Washer> Washers { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Связь Washer с Reservation (один ко многим)
            modelBuilder.Entity<Washer>()
                .HasMany(w => w.Reservations)
                .WithOne()
                .HasForeignKey(r => r.WasherId)
                .OnDelete(DeleteBehavior.Cascade); // Удаление резерваций при удалении машинки

            // Конфигурация для Reservation
            *//*modelBuilder.Entity<Reservation>()
                .HasOne<UserEF>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);*//* // Не позволяем удалять пользователя при наличии резерваций
        }*/

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

            if (!context.Washers.Any())
            {
                context.Washers.Add(new Washer
                {
                    Id = 1,
                    Name = "LG Vlad",
                    IsOccupied = false,
                });

                context.Washers.Add(new Washer
                {
                    Id = 2,
                    Name = "Samsung Sanek",
                    IsOccupied = false,
                });

                context.Washers.Add(new Washer
                {
                    Id = 3,
                    Name = "Apple Alya",
                    IsOccupied = false,
                });
            }

            if (!context.Reservations.Any())
            {
                context.Reservations.Add(new Reservation
                {
                    WasherId = 1,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddMinutes(120),
                    UserId = 0
                });
            }
            context.SaveChanges();
        }
    }
}
