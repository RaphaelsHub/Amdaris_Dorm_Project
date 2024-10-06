using Dorm.Domain.DTO.Chat;
using Dorm.Domain.Entities.Chat;
using System.Collections.Generic;
using Xunit;

namespace Dorm.Tests.DTO
{
    public class ChatDtoTests
    {
        [Fact]
        public void ChatDto_CanBeCreated_WithDefaultConstructor()
        {
            // Act
            var chatDto = new ChatDto();

            // Assert
            Assert.NotNull(chatDto);
        }

        [Fact]
        public void ChatDto_PropertiesCanBeSetAndGet()
        {
            // Arrange
            var chatDto = new ChatDto();
            var participants = new List<ChatUser>
            {
                new ChatUser { Id = 1, ChatId = 1, UserId = 1 },
                new ChatUser { Id = 2, ChatId = 1, UserId = 2 }
            };

            // Act
            chatDto.Id = 1;
            chatDto.Name = "Test Chat";
            chatDto.IsPrivate = true;
            chatDto.Participants = participants;

            // Assert
            Assert.Equal(1, chatDto.Id);
            Assert.Equal("Test Chat", chatDto.Name);
            Assert.True(chatDto.IsPrivate);
            Assert.Equal(participants, chatDto.Participants);
        }

        [Fact]
        public void ChatDto_ConstructorAssignsPropertiesCorrectly()
        {
            // Arrange
            var participants = new List<ChatUser>
            {
                new ChatUser { Id = 1, ChatId = 1, UserId = 1 },
                new ChatUser { Id = 2, ChatId = 1, UserId = 2 }
            };

            // Act
            var chatDto = new ChatDto
            {
                Id = 1,
                Name = "Test Chat",
                IsPrivate = true,
                Participants = participants
            };

            // Assert
            Assert.Equal(1, chatDto.Id);
            Assert.Equal("Test Chat", chatDto.Name);
            Assert.True(chatDto.IsPrivate);
            Assert.Equal(participants, chatDto.Participants);
        }

        [Fact]
        public void ChatDto_ParticipantsList_CanBeModified()
        {
            // Arrange
            var chatDto = new ChatDto
            {
                Participants = new List<ChatUser>()
            };

            var newParticipant = new ChatUser { Id = 1, ChatId = 1, UserId = 1 };

            // Act
            chatDto.Participants.Add(newParticipant);

            // Assert
            Assert.Single(chatDto.Participants);
            Assert.Equal(newParticipant, chatDto.Participants.First());
        }
    }
}
