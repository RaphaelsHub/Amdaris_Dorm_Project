using Dorm.Domain.Entities.Ad;
using Dorm.Domain.Enum.Ad;
using System;
using Xunit;

namespace Dorm.Tests
{
    public class AdTests
    {
        [Fact]
        public void CreateAd_WithValidData_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            int id = 1;
            int userId = 100;
            string name = "Test Ad";
            string number = "12345";
            AdType type = AdType.SALE; 
            AdStatus status = AdStatus.Actice; 
            string subject = "Ad Subject";
            string description = "Ad Description";
            decimal price = 99.99M;
            byte[] image = new byte[] { 1, 2, 3, 4 };
            DateTime createdDate = DateTime.Now;

            // Act
            var ad = new Ad
            {
                Id = id,
                UserId = userId,
                Name = name,
                Number = number,
                Type = type,
                Status = status,
                Subject = subject,
                Description = description,
                Price = price,
                Image = image,
                CreatedDate = createdDate
            };

            // Assert
            Assert.Equal(id, ad.Id);
            Assert.Equal(userId, ad.UserId);
            Assert.Equal(name, ad.Name);
            Assert.Equal(number, ad.Number);
            Assert.Equal(type, ad.Type);
            Assert.Equal(status, ad.Status);
            Assert.Equal(subject, ad.Subject);
            Assert.Equal(description, ad.Description);
            Assert.Equal(price, ad.Price);
            Assert.Equal(image, ad.Image);
            Assert.Equal(createdDate, ad.CreatedDate);
        }

        [Fact]
        public void Ad_DefaultValues_ShouldBeInitializedCorrectly()
        {
            // Act
            var ad = new Ad();

            // Assert
            Assert.Equal(0, ad.Id);
            Assert.Equal(0, ad.UserId);
            Assert.Null(ad.Name);
            Assert.Null(ad.Number);
            Assert.Equal(default(AdType), ad.Type); 
            Assert.Equal(AdStatus.Sold, ad.Status); 
            Assert.Null(ad.Subject);
            Assert.Null(ad.Description);
            Assert.Equal(0, ad.Price);
            Assert.Null(ad.Image);
            Assert.Equal(default(DateTime), ad.CreatedDate);
        }
    }
}
