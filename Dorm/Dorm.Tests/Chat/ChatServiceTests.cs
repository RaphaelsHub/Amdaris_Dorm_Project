using AutoMapper;
using Dorm.BLL.Services;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.DTO.Chat;
using Dorm.Domain.Entities.Chat;
using Dorm.Domain.Responces;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Dorm.Tests.Services
{
    public class ChatServiceTests
    {
        private readonly Mock<IChatRepository> _chatRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ChatService _chatService;

        public ChatServiceTests()
        {
            _chatRepositoryMock = new Mock<IChatRepository>();
            _mapperMock = new Mock<IMapper>();
            _chatService = new ChatService(_chatRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateChat_ReturnsSuccessResponse_WhenChatIsCreated()
        {
            // Arrange
            var chatDto = new ChatDto { Id = 1, Name = "Test Chat" };
            var chat = new Chat { Id = 1, Name = "Test Chat" };

            _mapperMock.Setup(m => m.Map<Chat>(chatDto)).Returns(chat);
            _chatRepositoryMock.Setup(repo => repo.Create(chat)).ReturnsAsync(true);

            // Act
            var result = await _chatService.Create(chatDto);

            // Assert
            Assert.True(result.Data);
            Assert.Equal("Success", result.Description);
        }

        [Fact]
        public async Task CreateChat_ReturnsErrorResponse_WhenExceptionIsThrown()
        {
            // Arrange
            var chatDto = new ChatDto { Id = 1, Name = "Test Chat" };

            _mapperMock.Setup(m => m.Map<Chat>(chatDto)).Throws(new Exception("Test exception"));

            // Act
            var result = await _chatService.Create(chatDto);

            // Assert
            Assert.False(result.Data);
            Assert.Equal("Test exception", result.Description);
        }

        [Fact]
        public async Task DeleteChat_ReturnsSuccessResponse_WhenChatIsDeleted()
        {
            // Arrange
            var chat = new Chat { Id = 1, Name = "Test Chat" };

            _chatRepositoryMock.Setup(repo => repo.GetById(chat.Id)).ReturnsAsync(chat);
            _chatRepositoryMock.Setup(repo => repo.Delete(chat)).ReturnsAsync(true);

            // Act
            var result = await _chatService.Delete(chat.Id);

            // Assert
            Assert.True(result.Data);
            Assert.Equal("Success.", result.Description);
        }

        [Fact]
        public async Task DeleteChat_ReturnsErrorResponse_WhenChatNotFound()
        {
            // Arrange
            _chatRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Chat)null);

            // Act
            var result = await _chatService.Delete(1);

            // Assert
            Assert.False(result.Data);
            Assert.Equal("chat not found.", result.Description);
        }

        [Fact]
        public async Task GetChatById_ReturnsChatDto_WhenChatExists()
        {
            // Arrange
            var chat = new Chat { Id = 1, Name = "Test Chat" };
            var chatDto = new ChatDto { Id = 1, Name = "Test Chat" };

            _chatRepositoryMock.Setup(repo => repo.GetById(chat.Id)).ReturnsAsync(chat);
            _mapperMock.Setup(m => m.Map<ChatDto>(chat)).Returns(chatDto);

            // Act
            var result = await _chatService.Get(chat.Id);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(chatDto.Name, result.Data.Name);
        }

        [Fact]
        public async Task GetChatById_ReturnsErrorResponse_WhenChatNotFound()
        {
            // Arrange
            _chatRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Chat)null);

            // Act
            var result = await _chatService.Get(1);

            // Assert
            Assert.Null(result.Data);
            Assert.Equal("chat not found.", result.Description);
        }

        [Fact]
        public async Task SaveMessage_ReturnsSuccessResponse_WhenMessageIsSaved()
        {
            // Arrange
            var messageDto = new ChatMessageDto { Id = 1, Content = "Test Message" };
            var message = new ChatMessage { Id = 1, Content = "Test Message", Timestamp = DateTime.UtcNow };

            _mapperMock.Setup(m => m.Map<ChatMessage>(messageDto)).Returns(message);
            _chatRepositoryMock.Setup(repo => repo.SaveMessage(message)).ReturnsAsync(true);

            // Act
            var result = await _chatService.SaveMessage(messageDto);

            // Assert
            Assert.True(result.Data);
            Assert.Equal("Success", result.Description);
        }

        [Fact]
        public async Task SaveMessage_ReturnsErrorResponse_WhenExceptionIsThrown()
        {
            // Arrange
            var messageDto = new ChatMessageDto { Id = 1, Content = "Test Message" };

            _mapperMock.Setup(m => m.Map<ChatMessage>(messageDto)).Throws(new Exception("Test exception"));

            // Act
            var result = await _chatService.SaveMessage(messageDto);

            // Assert
            Assert.False(result.Data);
            Assert.Equal("Test exception", result.Description);
        }
    }
}
