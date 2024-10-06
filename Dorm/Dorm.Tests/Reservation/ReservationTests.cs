using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dorm.Domain.Entities.Laundry;
using Xunit;

namespace Dorm.Tests.Domain.Entities.Laundry
{
    public class ReservationTests
    {
        [Fact]
        public void Reservation_ShouldSetAndGetPropertiesCorrectly()
        {
            // Arrange
            var id = 1;
            var washerId = 2;
            var userId = 123;
            var startTime = new DateTime(2024, 10, 6, 8, 0, 0); // 6th October 2024, 8:00 AM
            var endTime = new DateTime(2024, 10, 6, 9, 0, 0);   // 6th October 2024, 9:00 AM

            // Act
            var reservation = new Reservation
            {
                Id = id,
                WasherId = washerId,
                UserId = userId,
                StartTime = startTime,
                EndTime = endTime
            };

            // Assert
            Assert.Equal(id, reservation.Id);
            Assert.Equal(washerId, reservation.WasherId);
            Assert.Equal(userId, reservation.UserId);
            Assert.Equal(startTime, reservation.StartTime);
            Assert.Equal(endTime, reservation.EndTime);
        }

        [Fact]
        public void Reservation_ShouldHaveKeyAttribute()
        {
            // Arrange
            var propertyInfo = typeof(Reservation).GetProperty(nameof(Reservation.Id));

            // Act
            var attribute = propertyInfo.GetCustomAttributes(typeof(KeyAttribute), false);

            // Assert
            Assert.NotEmpty(attribute);
        }

        [Fact]
        public void Reservation_ShouldHaveDatabaseGeneratedAttribute()
        {
            // Arrange
            var propertyInfo = typeof(Reservation).GetProperty(nameof(Reservation.Id));

            // Act
            var attribute = propertyInfo.GetCustomAttributes(typeof(DatabaseGeneratedAttribute), false);

            // Assert
            Assert.NotEmpty(attribute);
            Assert.Equal(DatabaseGeneratedOption.Identity, ((DatabaseGeneratedAttribute)attribute[0]).DatabaseGeneratedOption);
        }
    }
}
