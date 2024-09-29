using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dorm.BLL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Enum.Ad;
using Dorm.Domain.Responces;
using Moq;
using Xunit;

namespace Dorm.BLL.Tests
{
    public class IAdServiceTests
    {
        private readonly Mock<IAdService> _adServiceMock;

        public IAdServiceTests()
        {
            _adServiceMock = new Mock<IAdService>();
        }

        [Fact]
        public async Task Create_ShouldReturnAdDto_WhenCalledWithValidModel()
        {
            // Arrange
            var adDto = new AdDto
            {
                UserId = 1,
                Name = "Test Ad",
                Number = "12345",
                Type = AdType.SALE,
                Status = AdStatus.Actice,
                Subject = "Test Subject",
                Description = "Test Description",
                Price = 100.00m,
                CreatedDate = DateTime.Now
            };

            var response = new TestsResponse<AdDto>(adDto, "Ad created successfully");

            _adServiceMock.Setup(service => service.Create(adDto)).ReturnsAsync(response);

            // Act
            var result = await _adServiceMock.Object.Create(adDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(adDto, result.Data);
            Assert.Equal("Ad created successfully", result.Description);
        }

        [Fact]
        public async Task Edit_ShouldReturnUpdatedAdDto_WhenCalledWithValidIdAndModel()
        {
            // Arrange
            var adId = 1;
            var adDto = new AdDto
            {
                UserId = 1,
                Name = "Updated Ad",
                Number = "12345",
                Type = AdType.BUY,
                Status = AdStatus.Actice,
                Subject = "Updated Subject",
                Description = "Updated Description",
                Price = 150.00m,
                CreatedDate = DateTime.Now
            };

            var response = new TestsResponse<AdDto>(adDto, "Ad updated successfully");

            _adServiceMock.Setup(service => service.Edit(adId, adDto)).ReturnsAsync(response);

            // Act
            var result = await _adServiceMock.Object.Edit(adId, adDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(adDto, result.Data);
            Assert.Equal("Ad updated successfully", result.Description);
        }

        [Fact]
        public async Task Get_ShouldReturnAdDto_WhenCalledWithValidId()
        {
            // Arrange
            var adId = 1;
            var adDto = new AdDto
            {
                Id = adId,
                UserId = 1,
                Name = "Test Ad",
                Number = "12345",
                Type = AdType.SALE,
                Status = AdStatus.Actice,
                Subject = "Test Subject",
                Description = "Test Description",
                Price = 100.00m,
                CreatedDate = DateTime.Now
            };

            var response = new TestsResponse<AdDto>(adDto, "Ad retrieved successfully");

            _adServiceMock.Setup(service => service.Get(adId)).ReturnsAsync(response);

            // Act
            var result = await _adServiceMock.Object.Get(adId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(adDto, result.Data);
            Assert.Equal("Ad retrieved successfully", result.Description);
        }
    }
}
