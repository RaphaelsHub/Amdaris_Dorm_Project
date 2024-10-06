using Dorm.DAL;
using Dorm.DAL.Repositories;
using Dorm.Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Dorm.Tests.Repositories
{
    public class ChatRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ChatRepository _repository;

        public ChatRepositoryTests()
        {
            _dbContext = GetDbContext().Result;
            _repository = new ChatRepository(_dbContext);
        }

        // Метод для создания базы данных каждого теста
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;
            var context = new ApplicationDbContext(options);
            await context.Database.EnsureCreatedAsync();
            return context;
        }

        // Метод для очистки базы данных после каждого теста
        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();  
            _dbContext.Dispose();
        }

        [Fact]
        public async Task CreateChat_AddsChatToDatabase()
        {
            // Arrange
            var chat = new Chat
            {
                Id = 1,
                Name = "Test Chat",
                IsPrivate = false
            };

            // Act
            var result = await _repository.Create(chat);
            var createdChat = await _dbContext.Chats.FirstOrDefaultAsync(c => c.Id == chat.Id);

            // Assert
            Assert.True(result);
            Assert.NotNull(createdChat);
            Assert.Equal(chat.Name, createdChat.Name);
        }

        [Fact]
        public async Task DeleteChat_RemovesChatFromDatabase()
        {
            // Arrange
            var chat = new Chat
            {
                Id = 1,
                Name = "Test Chat",
                IsPrivate = false
            };
            await _repository.Create(chat);

            // Act
            var result = await _repository.Delete(chat);
            var deletedChat = await _dbContext.Chats.FirstOrDefaultAsync(c => c.Id == chat.Id);

            // Assert
            Assert.True(result);
            Assert.Null(deletedChat);  
        }

        [Fact]
        public async Task GetChatById_ReturnsChat_WhenChatExists()
        {
            // Arrange
            var chat = new Chat
            {
                Id = 1,
                Name = "Test Chat",
                IsPrivate = false
            };
            await _repository.Create(chat);

            // Act
            var result = await _repository.GetById(chat.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(chat.Name, result.Name);
        }

        [Fact]
        public async Task GetAllChats_ReturnsAllChats()
        {
            // Arrange
            var chat1 = new Chat { Id = 1, Name = "Test Chat 1", IsPrivate = false };
            var chat2 = new Chat { Id = 2, Name = "Test Chat 2", IsPrivate = true };
            await _repository.Create(chat1);
            await _repository.Create(chat2);

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, await _dbContext.Chats.CountAsync());
        }

        [Fact]
        public async Task UpdateChat_UpdatesChatInDatabase()
        {
            // Arrange
            var chat = new Chat
            {
                Id = 1,
                Name = "Old Chat Name",
                IsPrivate = false
            };
            await _repository.Create(chat);

            // Act
            chat.Name = "Updated Chat Name";
            var result = await _repository.Update(chat);
            var updatedChat = await _dbContext.Chats.FirstOrDefaultAsync(c => c.Id == chat.Id);

            // Assert
            Assert.Equal("Updated Chat Name", updatedChat.Name);
        }
    }
}
