using Dorm.Domain.Entities.Chat;
using System.Collections.Generic;
using Xunit;

namespace Dorm.Tests.Domain.Entities
{
    public class ChatTests
    {
        [Fact]
        public void Chat_Initialization_SetsPropertiesCorrectly()
        {
            // Arrange
            var chat = new Chat
            {
                Id = 1,
                Name = "Test Chat",
                IsPrivate = true,
                Participants = new List<ChatUser>()
            };

            // Act & Assert
            Assert.Equal(1, chat.Id);
            Assert.Equal("Test Chat", chat.Name);
            Assert.True(chat.IsPrivate);
            Assert.NotNull(chat.Participants);
            Assert.Empty(chat.Participants);
        }

        [Fact]
        public void Chat_AddParticipant_AddsUserToParticipants()
        {
            // Arrange
            var chat = new Chat
            {
                Id = 1,
                Name = "Test Chat",
                IsPrivate = false,
                Participants = new List<ChatUser>()
            };

            var user = new ChatUser
            {
                UserId = 1,
                ChatId = 1
            };

            // Act
            chat.Participants.Add(user);

            // Assert
            Assert.Single(chat.Participants);
            Assert.Contains(user, chat.Participants);
        }
    }
}
