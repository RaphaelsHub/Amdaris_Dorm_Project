using Dorm.Domain.DTO.Chat;
using System;
using Xunit;

namespace Dorm.Tests.DTO
{
    public class ChatMessageDtoTests
    {
        [Fact]
        public void ChatMessageDto_CanBeCreated_WithDefaultConstructor()
        {
            // Act
            var chatMessageDto = new ChatMessageDto();

            // Assert
            Assert.NotNull(chatMessageDto);
        }

        [Fact]
        public void ChatMessageDto_PropertiesCanBeSetAndGet()
        {
            // Arrange
            var chatMessageDto = new ChatMessageDto();
            var timestamp = DateTime.UtcNow;

            // Act
            chatMessageDto.Id = 1;
            chatMessageDto.ChatId = 2;
            chatMessageDto.UserInfo = "User123";
            chatMessageDto.Content = "Hello, World!";
            chatMessageDto.Timestamp = timestamp;

            // Assert
            Assert.Equal(1, chatMessageDto.Id);
            Assert.Equal(2, chatMessageDto.ChatId);
            Assert.Equal("User123", chatMessageDto.UserInfo);
            Assert.Equal("Hello, World!", chatMessageDto.Content);
            Assert.Equal(timestamp, chatMessageDto.Timestamp);
        }

        [Fact]
        public void ChatMessageDto_ConstructorAssignsPropertiesCorrectly()
        {
            // Arrange
            var timestamp = DateTime.UtcNow;

            // Act
            var chatMessageDto = new ChatMessageDto
            {
                Id = 1,
                ChatId = 2,
                UserInfo = "User456",
                Content = "Test message",
                Timestamp = timestamp
            };

            // Assert
            Assert.Equal(1, chatMessageDto.Id);
            Assert.Equal(2, chatMessageDto.ChatId);
            Assert.Equal("User456", chatMessageDto.UserInfo);
            Assert.Equal("Test message", chatMessageDto.Content);
            Assert.Equal(timestamp, chatMessageDto.Timestamp);
        }
    }
}
