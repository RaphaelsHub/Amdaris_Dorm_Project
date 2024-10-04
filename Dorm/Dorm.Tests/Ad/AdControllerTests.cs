using Dorm.Server.Controllers;
using Dorm.Domain.DTO;
using Dorm.Domain.Responces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Dorm.Server.Contracts.Commands.Ad.Create;
using Dorm.Server.Contracts.Commands.Ad.Delete;
using Dorm.Server.Contracts.Commands.Ad.Edit;
using Dorm.Server.Contracts.Queries.Ad.Get;
using Dorm.Server.Contracts.Queries.Ad.GetAll;

public class AdControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly AdController _controller;

    public AdControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new AdController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Create_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var adDto = new AdDto { Name = "Test Ad", Price = 100.0m };
        var token = "testToken";

        var context = new DefaultHttpContext();
        context.Request.Headers["Cookie"] = $"authToken={token}";

        _controller.ControllerContext.HttpContext = context;

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateAdCommand>(), default))
            .ReturnsAsync(new BaseResponse<AdDto>(adDto, null));

        // Act
        var result = await _controller.Create(adDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(adDto, okResult.Value);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenFailed()
    {
        // Arrange
        var adDto = new AdDto { Name = "Test Ad", Price = 100.0m };
        var token = "testToken";

        var context = new DefaultHttpContext();
        context.Request.Headers["Cookie"] = $"authToken={token}";

        _controller.ControllerContext.HttpContext = context;

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateAdCommand>(), default))
            .ReturnsAsync(new BaseResponse<AdDto>(null, "Error creating ad"));

        // Act
        var result = await _controller.Create(adDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error creating ad", badRequestResult.Value);
    }

    [Fact]
    public async Task Get_ReturnsOk_WhenAdExists()
    {
        // Arrange
        var adId = 1;
        var token = "testToken";
        var adDto = new AdDto { Name = "Test Ad", Price = 100.0m };

        var context = new DefaultHttpContext();
        context.Request.Headers["Cookie"] = $"authToken={token}";

        _controller.ControllerContext.HttpContext = context;

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAdQuery>(), default))
            .ReturnsAsync(new BaseResponse<AdDto>(adDto, null));

        // Act
        var result = await _controller.Get(adId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(adDto, okResult.Value);
    }

    [Fact]
    public async Task Get_ReturnsBadRequest_WhenAdDoesNotExist()
    {
        // Arrange
        var adId = 1;
        var token = "testToken";

        var context = new DefaultHttpContext();
        context.Request.Headers["Cookie"] = $"authToken={token}";

        _controller.ControllerContext.HttpContext = context;

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAdQuery>(), default))
            .ReturnsAsync(new BaseResponse<AdDto>(null, "Ad not found"));

        // Act
        var result = await _controller.Get(adId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Ad not found", badRequestResult.Value);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WhenAdsExist()
    {
        // Arrange
        var ads = new List<AdDto>
    {
        new AdDto { Name = "Test Ad 1", Price = 100.0m },
        new AdDto { Name = "Test Ad 2", Price = 200.0m }
    };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllAdsQuery>(), default))
            .ReturnsAsync(new BaseResponse<IEnumerable<AdDto>>(ads, null)); 

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(ads, okResult.Value);
    }

    [Fact]
    public async Task GetAll_ReturnsBadRequest_WhenNoAdsExist()
    {
        // Arrange
        var expectedResponse = new BaseResponse<IEnumerable<AdDto>>(null, "No ads found");
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllAdsQuery>(), default))
            .ReturnsAsync(expectedResponse); 

        // Act
        var result = await _controller.GetAll();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("No ads found", badRequestResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsOk_WhenAdDeleted()
    {
        // Arrange
        var adId = 1;

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteAdCommand>(), default))
            .ReturnsAsync(new BaseResponse<bool>(true, null));

        // Act
        var result = await _controller.Delete(adId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)okResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsBadRequest_WhenAdNotDeleted()
    {
        // Arrange
        var adId = 1;

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteAdCommand>(), default))
            .ReturnsAsync(new BaseResponse<bool>(false, "Error deleting ad"));

        // Act
        var result = await _controller.Delete(adId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error deleting ad", badRequestResult.Value);
    }

    [Fact]
    public async Task Edit_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var adId = 1;
        var adDto = new AdDto { Name = "Updated Ad", Price = 150.0m };
        var token = "testToken";

        var context = new DefaultHttpContext();
        context.Request.Headers["Cookie"] = $"authToken={token}";

        _controller.ControllerContext.HttpContext = context;

        _mediatorMock.Setup(m => m.Send(It.IsAny<EditAdCommand>(), default))
            .ReturnsAsync(new BaseResponse<AdDto>(adDto, null));

        // Act
        var result = await _controller.Edit(adId, adDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(adDto, okResult.Value);
    }

    [Fact]
    public async Task Edit_ReturnsBadRequest_WhenFailed()
    {
        // Arrange
        var adId = 1;
        var adDto = new AdDto { Name = "Updated Ad", Price = 150.0m };
        var token = "testToken";

        var context = new DefaultHttpContext();
        context.Request.Headers["Cookie"] = $"authToken={token}";

        _controller.ControllerContext.HttpContext = context;

        _mediatorMock.Setup(m => m.Send(It.IsAny<EditAdCommand>(), default))
            .ReturnsAsync(new BaseResponse<AdDto>(null, "Error updating ad"));

        // Act
        var result = await _controller.Edit(adId, adDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error updating ad", badRequestResult.Value);
    }
}
