using Dorm.BLL.Interfaces;
using Dorm.BLL.Settings;
using Dorm.Domain.DTO.Chat;
using Dorm.Domain.Responces;
using Dorm.Server.Controllers;
using Dorm.Server.Hubs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Dorm.Tests.Controllers
{
    public class ChatControllerTests
    {
        private readonly Mock<IChatService> _chatServiceMock;
        private readonly Mock<IHubContext<ChatHub>> _hubContextMock;
        private readonly Mock<IOptions<AuthSettings>> _optionsMock;
        private readonly ChatController _controller;

        public ChatControllerTests()
        {
            _chatServiceMock = new Mock<IChatService>();
            _hubContextMock = new Mock<IHubContext<ChatHub>>();
            _optionsMock = new Mock<IOptions<AuthSettings>>();
            _controller = new ChatController(_chatServiceMock.Object, _hubContextMock.Object, _optionsMock.Object);
        }

        [Fact]
        public async Task GetMessages_ReturnsOk_WithMessageData()
        {
            // Arrange
            var messages = new List<ChatMessageDto>
            {
                new ChatMessageDto { Id = 1, ChatId = 1, Content = "Hello" }
            };
            var response = new BaseResponse<IEnumerable<ChatMessageDto>>(messages, "Success");
            _chatServiceMock.Setup(s => s.GetAllMessages(0)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetMessages();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedMessages = Assert.IsAssignableFrom<IEnumerable<ChatMessageDto>>(okResult.Value);
            Assert.Single(returnedMessages);
        }

        [Fact]
        public async Task SendMessage_ReturnsOk_WhenMessageIsSaved()
        {
            // Arrange
            var message = new ChatMessageDto { ChatId = 1, Content = "Hello" };
            var response = new BaseResponse<bool>(true, "Success");
            _chatServiceMock.Setup(s => s.SaveMessage(message)).ReturnsAsync(response);

            // Act
            var result = await _controller.SendMessage(message);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task SendMessage_ReturnsBadRequest_WhenMessageIsNotSaved()
        {
            // Arrange
            var message = new ChatMessageDto { ChatId = 1, Content = "Hello" };
            var response = new BaseResponse<bool>(false, "Error");
            _chatServiceMock.Setup(s => s.SaveMessage(message)).ReturnsAsync(response);

            // Act
            var result = await _controller.SendMessage(message);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteMessage_ReturnsOk_WhenMessageIsDeleted()
        {
            // Arrange
            var response = new BaseResponse<bool>(true, "Success");
            _chatServiceMock.Setup(s => s.DeleteMessage(1)).ReturnsAsync(response);

            var hubClientsMock = new Mock<IHubClients>();
            var clientProxyMock = new Mock<IClientProxy>();

            hubClientsMock.Setup(c => c.All).Returns(clientProxyMock.Object);
            _hubContextMock.Setup(h => h.Clients).Returns(hubClientsMock.Object);

            // Act
            var result = await _controller.DeleteMessage(1);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
           
            clientProxyMock.Verify(
                client => client.SendCoreAsync("DeleteMessage", It.Is<object[]>(o => (int)o[0] == 1), default),
                Times.Once
            );
        }


        [Fact]
        public async Task DeleteMessage_ReturnsBadRequest_WhenMessageIsNotDeleted()
        {
            // Arrange
            var response = new BaseResponse<bool>(false, "Error");
            _chatServiceMock.Setup(s => s.DeleteMessage(1)).ReturnsAsync(response);

            // Act
            var result = await _controller.DeleteMessage(1);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequestResult.Value);
        }

        [Fact]
        public async Task EditMessage_ReturnsOk_WhenMessageIsUpdated()
        {
            // Arrange
            var updatedMessageDto = new ChatMessageDto { Id = 1, Content = "Updated message" };
            var response = new BaseResponse<bool>(true, "Success");

            _chatServiceMock.Setup(s => s.EditMessage(1, updatedMessageDto)).ReturnsAsync(response);

            var hubClientsMock = new Mock<IHubClients>();
            var clientProxyMock = new Mock<IClientProxy>();

            hubClientsMock.Setup(c => c.All).Returns(clientProxyMock.Object);
            _hubContextMock.Setup(h => h.Clients).Returns(hubClientsMock.Object);

            // Act
            var result = await _controller.EditMessage(1, updatedMessageDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);

            clientProxyMock.Verify(
                client => client.SendCoreAsync("UpdateMessage", It.Is<object[]>(o => (int)o[0] == 1 && (string)o[1] == "Updated message"), default),
                Times.Once
            );
        }


        [Fact]
        public async Task EditMessage_ReturnsBadRequest_WhenMessageIsNotUpdated()
        {
            // Arrange
            var message = new ChatMessageDto { Id = 1, ChatId = 1, Content = "Updated message" };
            var response = new BaseResponse<bool>(false, "Error");
            _chatServiceMock.Setup(s => s.EditMessage(1, message)).ReturnsAsync(response);

            // Act
            var result = await _controller.EditMessage(1, message);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequestResult.Value);
        }
    }
}
