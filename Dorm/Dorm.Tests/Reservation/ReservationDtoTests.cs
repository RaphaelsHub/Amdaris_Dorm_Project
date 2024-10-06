using System;
using Dorm.Domain.DTO.Laundry;
using Xunit;

namespace Dorm.Tests.Domain.DTO.Laundry
{
    public class ReservationDtoTests
    {
        [Fact]
        public void ReservationDto_ShouldSetAndGetPropertiesCorrectly()
        {
            // Arrange
            var washerId = 1;
            var userId = 123;
            var startTime = new DateTime(2024, 10, 6, 8, 0, 0); // 6th October 2024, 8:00 AM
            var endTime = new DateTime(2024, 10, 6, 9, 0, 0);   // 6th October 2024, 9:00 AM

            // Act
            var reservation = new ReservationDto
            {
                WasherId = washerId,
                UserId = userId,
                StartTime = startTime,
                EndTime = endTime
            };

            // Assert
            Assert.Equal(washerId, reservation.WasherId);
            Assert.Equal(userId, reservation.UserId);
            Assert.Equal(startTime, reservation.StartTime);
            Assert.Equal(endTime, reservation.EndTime);
        }
    }
}
