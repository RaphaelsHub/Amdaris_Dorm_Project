using Dorm.Domain.Entities.Chat;
using Xunit;

namespace Dorm.Tests.Domain.Entities
{
    public class ChatUserTests
    {
        [Fact]
        public void ChatUser_CanBeCreated_WithDefaultConstructor()
        {
            // Act
            var chatUser = new ChatUser();

            // Assert
            Assert.NotNull(chatUser);
        }

        [Fact]
        public void ChatUser_PropertiesCanBeSetAndGet()
        {
            // Arrange
            var chatUser = new ChatUser();

            // Act
            chatUser.Id = 1;
            chatUser.ChatId = 2;
            chatUser.UserId = 3;

            // Assert
            Assert.Equal(1, chatUser.Id);
            Assert.Equal(2, chatUser.ChatId);
            Assert.Equal(3, chatUser.UserId);
        }

        [Fact]
        public void ChatUser_ConstructorAssignsPropertiesCorrectly()
        {
            // Arrange & Act
            var chatUser = new ChatUser
            {
                Id = 1,
                ChatId = 2,
                UserId = 3
            };

            // Assert
            Assert.Equal(1, chatUser.Id);
            Assert.Equal(2, chatUser.ChatId);
            Assert.Equal(3, chatUser.UserId);
        }
    }
}
