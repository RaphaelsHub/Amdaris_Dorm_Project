using Dorm.DAL;
using Dorm.DAL.Repositories;
using Dorm.Domain.Entities.Ad;
using Dorm.Domain.Enum.Ad;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class AdRepositoryTests
{
    private readonly AdRepository _repository;
    private readonly ApplicationDbContext _context;

    public AdRepositoryTests()
    {
        // Создаем новый экземпляр базы данных для каждого теста
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Используем уникальное имя базы данных
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new AdRepository(_context);
    }

    [Fact]
    public async Task Create_AddsAdToDatabase()
    {
        // Arrange
        var ad = new Ad
        {
            Id = 1,
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

        // Act
        var result = await _repository.Create(ad);

        // Assert
        Assert.True(result);
        Assert.Equal(1, await _context.Ads.CountAsync());
    }

    [Fact]
    public async Task Delete_RemovesAdFromDatabase()
    {
        // Arrange
        var ad = new Ad
        {
            Id = 1,
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
        await _repository.Create(ad);

        // Act
        var result = await _repository.Delete(ad);

        // Assert
        Assert.True(result);
        Assert.Equal(0, await _context.Ads.CountAsync());
    }

    [Fact]
    public async Task GetAll_ReturnsAllAds()
    {
        // Arrange
        await _repository.Create(new Ad
        {
            Id = 1,
            UserId = 1,
            Name = "Ad 1",
            Number = "12345",
            Type = AdType.BUY,
            Status = AdStatus.Actice,
            Subject = "Subject 1",
            Description = "Description 1",
            Price = 200.00m,
            CreatedDate = DateTime.Now
        });

        await _repository.Create(new Ad
        {
            Id = 2,
            UserId = 1,
            Name = "Ad 2",
            Number = "0987654321",
            Type = AdType.SALE,
            Status = AdStatus.Actice,
            Subject = "Subject 2",
            Description = "Description 2",
            Price = 300.00m,
            CreatedDate = DateTime.Now
        });

        // Act
        var ads = await _repository.GetAll();

        // Assert
        Assert.Equal(2, ads.Count());
    }

    [Fact]
    public async Task GetById_ReturnsAd()
    {
        // Arrange
        var ad = new Ad
        {
            Id = 1,
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
        await _repository.Create(ad);

        // Act
        var result = await _repository.GetById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Ad", result.Name);
        Assert.Equal(AdType.SALE, result.Type);
        Assert.Equal(AdStatus.Actice, result.Status);
    }

    [Fact]
    public async Task Update_UpdatesAdInDatabase()
    {
        // Arrange
        var ad = new Ad
        {
            Id = 1,
            UserId = 1,
            Name = "Old Name",
            Number = "12345",
            Type = AdType.SALE,
            Status = AdStatus.Actice,
            Subject = "Test Subject",
            Description = "Test Description",
            Price = 100.00m,
            CreatedDate = DateTime.Now
        };
        await _repository.Create(ad);
        ad.Name = "New Name";
        ad.Status = AdStatus.Sold;

        // Act
        var updatedAd = await _repository.Update(ad);

        // Assert
        Assert.Equal("New Name", updatedAd.Name);
        Assert.Equal(AdStatus.Sold, updatedAd.Status);
        var result = await _repository.GetById(1);
        Assert.Equal("New Name", result.Name);
        Assert.Equal(AdStatus.Sold, result.Status);
    }
}
