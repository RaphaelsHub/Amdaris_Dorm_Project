using Dorm.Domain.DTO.Chat;
using Xunit;

namespace Dorm.Tests.DTO
{
    public class ChatUserDtoTests
    {
        [Fact]
        public void ChatUserDto_CanBeCreated_WithDefaultConstructor()
        {
            // Act
            var chatUserDto = new ChatUserDto();

            // Assert
            Assert.NotNull(chatUserDto);
        }

        [Fact]
        public void ChatUserDto_PropertiesCanBeSetAndGet()
        {
            // Arrange
            var chatUserDto = new ChatUserDto();

            // Act
            chatUserDto.Id = 1;
            chatUserDto.ChatId = 2;
            chatUserDto.UserId = "User123";

            // Assert
            Assert.Equal(1, chatUserDto.Id);
            Assert.Equal(2, chatUserDto.ChatId);
            Assert.Equal("User123", chatUserDto.UserId);
        }

        [Fact]
        public void ChatUserDto_ConstructorAssignsPropertiesCorrectly()
        {
            // Act
            var chatUserDto = new ChatUserDto
            {
                Id = 1,
                ChatId = 2,
                UserId = "User456"
            };

            // Assert
            Assert.Equal(1, chatUserDto.Id);
            Assert.Equal(2, chatUserDto.ChatId);
            Assert.Equal("User456", chatUserDto.UserId);
        }
    }
}
