using AutoMapper;
using Dorm.BLL.Interfaces;
using Dorm.BLL.Services;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ad;
using Dorm.Domain.Enum.Ad;
using Dorm.Domain.Responces;
using Dorm.Domain.Responses;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dorm.BLL.Tests
{
    public class AdServiceTests
    {
        private readonly Mock<IAdRepository> _adRepositoryMock;
        private readonly IMapper _mapper;
        private readonly IAdService _adService;

        public AdServiceTests()
        {
            _adRepositoryMock = new Mock<IAdRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AdDto, Ad>().ReverseMap();
            });

            _mapper = config.CreateMapper();
            _adService = new AdService(_adRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Create_ValidAd_ReturnsSuccessResponse()
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
                Price = 100,
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var result = await _adService.Create(adDto);
            var testsResponse = new TestsResponse<AdDto>(result.Data, result.Description);

            // Assert
            Assert.True(testsResponse.Success);
            Assert.Equal("Success", result.Description);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task Delete_ExistingAd_ReturnsSuccessResponse()
        {
            // Arrange
            var adId = 1;
            var ad = new Ad { Id = adId };

            // Настройка мока для получения существующей рекламы
            _adRepositoryMock.Setup(repo => repo.GetById(adId)).ReturnsAsync(ad);

            // Настройка мока для удаления рекламы, возвращая true
            _adRepositoryMock.Setup(repo => repo.Delete(ad)).ReturnsAsync(true);

            // Act
            var result = await _adService.Delete(adId);
            var testsResponse = new TestsResponse<bool>(result.Data, result.Description);

            // Assert
            Assert.True(testsResponse.Success);
            Assert.Equal("Success.", result.Description);
        }


        [Fact]
        public async Task Delete_NonExistingAd_ReturnsFailureResponse()
        {
            // Arrange
            var adId = 1;

            _adRepositoryMock.Setup(repo => repo.GetById(adId)).ReturnsAsync((Ad)null);

            // Act
            var result = await _adService.Delete(adId);

            // Assert
            Assert.False(result.Data);
            Assert.Equal("ad not found.", result.Description);
        }

        [Fact]
        public async Task Edit_ExistingAd_ReturnsSuccessResponse()
        {
            // Arrange
            var adId = 1;
            var existingAd = new Ad { Id = adId, CreatedDate = DateTime.UtcNow };
            var updatedAdDto = new AdDto { Id = adId, Name = "Updated Ad" };

            // Настройка мока для получения существующей рекламы
            _adRepositoryMock.Setup(repo => repo.GetById(adId)).ReturnsAsync(existingAd);

            // Настройка мока для обновления рекламы, возвращая обновленный объект
            _adRepositoryMock.Setup(repo => repo.Update(It.IsAny<Ad>())).ReturnsAsync(existingAd);

            // Act
            var result = await _adService.Edit(adId, updatedAdDto);
            var testsResponse = new TestsResponse<AdDto>(result.Data, result.Description);

            // Assert
            Assert.True(testsResponse.Success);
            Assert.Equal("Success", result.Description);
            Assert.NotNull(result.Data);
        }


        [Fact]
        public async Task Get_ExistingAd_ReturnsSuccessResponse()
        {
            // Arrange
            var adId = 1;
            var ad = new Ad { Id = adId, Name = "Test Ad" };

            _adRepositoryMock.Setup(repo => repo.GetById(adId)).ReturnsAsync(ad);

            // Act
            var result = await _adService.Get(adId);
            var testsResponse = new TestsResponse<AdDto>(result.Data, result.Description);

            // Assert
            Assert.True(testsResponse.Success);
            Assert.Equal("Success.", result.Description);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetAll_ReturnsAllAds()
        {
            // Arrange
            var ads = new List<Ad>
            {
                new Ad { Id = 1, Name = "Ad 1" },
                new Ad { Id = 2, Name = "Ad 2" }
            };

            _adRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(ads);

            // Act
            var result = await _adService.GetAll();

            var testsResponse = new TestsResponse<IEnumerable<AdDto>>(result.Data, result.Description);

            // Assert
            Assert.True(testsResponse.Success);
            Assert.Equal("Success.", result.Description);
            Assert.Equal(2, result.Data.Count());
        }
    }
}
