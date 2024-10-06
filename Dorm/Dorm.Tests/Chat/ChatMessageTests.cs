using Dorm.Domain.Entities.Chat;
using System;
using Xunit;

namespace Dorm.Tests.Domain.Entities
{
    public class ChatMessageTests
    {
        [Fact]
        public void ChatMessage_CanBeCreated_WithDefaultConstructor()
        {
            // Act
            var chatMessage = new ChatMessage();

            // Assert
            Assert.NotNull(chatMessage);
        }

        [Fact]
        public void ChatMessage_PropertiesCanBeSetAndGet()
        {
            // Arrange
            var chatMessage = new ChatMessage();
            var timestamp = DateTime.UtcNow;

            // Act
            chatMessage.Id = 1;
            chatMessage.ChatId = 2;
            chatMessage.UserInfo = "User123";
            chatMessage.Content = "Hello, this is a test message!";
            chatMessage.Timestamp = timestamp;

            // Assert
            Assert.Equal(1, chatMessage.Id);
            Assert.Equal(2, chatMessage.ChatId);
            Assert.Equal("User123", chatMessage.UserInfo);
            Assert.Equal("Hello, this is a test message!", chatMessage.Content);
            Assert.Equal(timestamp, chatMessage.Timestamp);
        }

        [Fact]
        public void ChatMessage_ConstructorAssignsPropertiesCorrectly()
        {
            // Arrange
            var timestamp = DateTime.UtcNow;

            // Act
            var chatMessage = new ChatMessage
            {
                Id = 1,
                ChatId = 2,
                UserInfo = "User123",
                Content = "This is another test message.",
                Timestamp = timestamp
            };

            // Assert
            Assert.Equal(1, chatMessage.Id);
            Assert.Equal(2, chatMessage.ChatId);
            Assert.Equal("User123", chatMessage.UserInfo);
            Assert.Equal("This is another test message.", chatMessage.Content);
            Assert.Equal(timestamp, chatMessage.Timestamp);
        }
    }
}
