using Dorm.DAL;
using Dorm.DAL.Repositories;
using Dorm.Domain.Entities.Laundry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dorm.Tests.Repositories
{
    public class WasherRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly WasherRepository _repository;

        public WasherRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _repository = new WasherRepository(_dbContext);
        }

        [Fact]
        public async Task Create_ShouldAddReservation()
        {
            // Arrange
            await ClearDbAsync();
            var reservation = new Reservation
            {
                WasherId = 1,
                UserId = 1,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            };

            // Act
            var result = await _repository.Create(reservation);

            // Assert
            Assert.True(result);
            Assert.Equal(1, await _dbContext.Reservations.CountAsync());
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllReservations()
        {
            // Arrange
            await ClearDbAsync();
            var reservation1 = new Reservation { WasherId = 1, UserId = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) };
            var reservation2 = new Reservation { WasherId = 2, UserId = 2, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(2) };
            await _repository.Create(reservation1);
            await _repository.Create(reservation2);

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetById_ShouldReturnReservation_WhenExists()
        {
            // Arrange
            await ClearDbAsync();
            var reservation = new Reservation { WasherId = 1, UserId = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) };
            await _repository.Create(reservation);

            // Act
            var result = await _repository.GetById(reservation.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reservation.Id, result.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            await ClearDbAsync();

            // Act
            var result = await _repository.GetById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldRemoveReservation_WhenExists()
        {
            // Arrange
            await ClearDbAsync();
            var reservation = new Reservation { WasherId = 1, UserId = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) };
            await _repository.Create(reservation);

            // Act
            var result = await _repository.Delete(reservation);

            // Assert
            Assert.True(result);
            Assert.Equal(0, await _dbContext.Reservations.CountAsync());
        }

        [Fact]
        public async Task GetAllByUserId_ShouldReturnUserReservations()
        {
            // Arrange
            await ClearDbAsync();
            var reservation1 = new Reservation { WasherId = 1, UserId = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) };
            var reservation2 = new Reservation { WasherId = 2, UserId = 1, StartTime = DateTime.Now.AddHours(2), EndTime = DateTime.Now.AddHours(3) };
            var reservation3 = new Reservation { WasherId = 3, UserId = 2, StartTime = DateTime.Now.AddHours(4), EndTime = DateTime.Now.AddHours(5) };

            await _repository.Create(reservation1);
            await _repository.Create(reservation2);
            await _repository.Create(reservation3);

            // Act
            var result = await _repository.GetAllByUserId(1);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Update_ShouldModifyReservation_WhenExists()
        {
            // Arrange
            await ClearDbAsync();
            var reservation = new Reservation
            {
                WasherId = 1,
                UserId = 1,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            };
            await _repository.Create(reservation);
            reservation.EndTime = DateTime.Now.AddHours(2);

            // Act
            var updatedReservation = await _repository.Update(reservation);

            // Assert
            Assert.NotNull(updatedReservation);
            Assert.Equal(reservation.EndTime, updatedReservation.EndTime);

            var retrievedReservation = await _repository.GetById(reservation.Id);
            Assert.Equal(reservation.EndTime, retrievedReservation.EndTime);
        }

        [Fact]
        public async Task GetAllByWasherId_ShouldReturnWasherReservations()
        {
            // Arrange
            await ClearDbAsync();
            var reservation1 = new Reservation { WasherId = 1, UserId = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) };
            var reservation2 = new Reservation { WasherId = 1, UserId = 2, StartTime = DateTime.Now.AddHours(2), EndTime = DateTime.Now.AddHours(3) };
            var reservation3 = new Reservation { WasherId = 2, UserId = 1, StartTime = DateTime.Now.AddHours(4), EndTime = DateTime.Now.AddHours(5) };

            await _repository.Create(reservation1);
            await _repository.Create(reservation2);
            await _repository.Create(reservation3);

            // Act
            var result = await _repository.GetAllByWasherId(1);

            // Assert
            Assert.Equal(2, result.Count());
        }

        private async Task ClearDbAsync()
        {
            _dbContext.Reservations.RemoveRange(_dbContext.Reservations);
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
