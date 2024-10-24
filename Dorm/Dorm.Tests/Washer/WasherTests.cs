using System;
using Dorm.Domain.Entities.Laundry;
using Xunit;

namespace Dorm.Tests
{
    public class WasherTests
    {
        [Fact]
        public void Washer_Should_Set_Properties_Correctly()
        {
            // Arrange
            var washer = new Washer
            {
                Id = 1,
                Name = "Washer 1",
                IsOccupied = false
            };

            // Act
            washer.IsOccupied = true; 

            // Assert
            Assert.Equal(1, washer.Id);
            Assert.Equal("Washer 1", washer.Name);
            Assert.True(washer.IsOccupied);
        }

        [Fact]
        public void Washer_Should_Be_Initialized_Correctly()
        {
            // Arrange & Act
            var washer = new Washer();

            // Assert
            Assert.Equal(0, washer.Id); 
            Assert.Null(washer.Name); 
            Assert.False(washer.IsOccupied); 
        }
    }
}
