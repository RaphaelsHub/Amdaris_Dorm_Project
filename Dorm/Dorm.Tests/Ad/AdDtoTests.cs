using System;
using Dorm.Domain.DTO;
using Dorm.Domain.Enum.Ad;
using Xunit;

namespace Dorm.Tests
{
    public class AdDtoTests
    {
        [Fact]
        public void AdDto_Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var adDto = new AdDto
            {
                Id = 1,
                UserId = 123,
                Name = "Test Ad",
                Number = "12345",
                Type = AdType.SALE,
                Status = AdStatus.Actice,
                Subject = "Test Subject",
                Description = "Test Description",
                Price = 99.99m,
                Image = null,
                CreatedDate = DateTime.UtcNow,
                CanEdit = true
            };

            // Act & Assert
            Assert.Equal(1, adDto.Id);
            Assert.Equal(123, adDto.UserId);
            Assert.Equal("Test Ad", adDto.Name);
            Assert.Equal("12345", adDto.Number);
            Assert.Equal(AdType.SALE, adDto.Type);
            Assert.Equal(AdStatus.Actice, adDto.Status);
            Assert.Equal("Test Subject", adDto.Subject);
            Assert.Equal("Test Description", adDto.Description);
            Assert.Equal(99.99m, adDto.Price);
            Assert.Null(adDto.Image);
            Assert.True(adDto.CanEdit);
        }

        [Fact]
        public void AdDto_CanEdit_ShouldBeTrue_WhenInitialized()
        {
            // Arrange
            var adDto = new AdDto { CanEdit = true };

            // Act
            bool canEdit = adDto.CanEdit;

            // Assert
            Assert.True(canEdit);
        }

        [Fact]
        public void AdDto_Price_ShouldBePositive()
        {
            // Arrange
            var adDto = new AdDto { Price = 100.00m };

            // Act
            bool isPricePositive = adDto.Price > 0;

            // Assert
            Assert.True(isPricePositive);
        }

        [Fact]
        public void AdDto_Status_ShouldBeActive_WhenInitialized()
        {
            // Arrange
            var adDto = new AdDto { Status = AdStatus.Actice };

            // Act
            var status = adDto.Status;

            // Assert
            Assert.Equal(AdStatus.Actice, status);
        }

        [Fact]
        public void AdDto_Type_ShouldBeSale_WhenInitialized()
        {
            // Arrange
            var adDto = new AdDto { Type = AdType.SALE };

            // Act
            var type = adDto.Type;

            // Assert
            Assert.Equal(AdType.SALE, type);
        }
    }
}
