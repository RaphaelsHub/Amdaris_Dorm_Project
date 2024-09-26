using Dorm.DAL.Interfaces;
using Dorm.Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.DAL.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _db;
        public ChatRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Chat entity)
        {
            _db.Chats.Add(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Chat entity)
        {
            _db.Chats.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMessage(ChatMessage entity)
        {
            _db.ChatMessages.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Chat>> GetAll()
        {
            return await _db.Chats.Include(c => c.Participants).ToListAsync();
        }

        public async Task<IEnumerable<ChatMessage>> GetAllMessages(int id)
        {
            return await _db.ChatMessages
            .Where(m => m.ChatId == id)
            //.OrderByDescending(m => m.Timestamp)
            .ToListAsync();
        }

        public async Task<IEnumerable<Chat>> GetAllСhatsByUserId(int id)
        {
            return await _db.Chats.Include(c => c.Participants).Where(c => c.Id == id).ToListAsync();

        }

        public async Task<Chat?> GetById(int id)
        {
            return await _db.Chats
            .Include(c => c.Participants)
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ChatMessage> GetMessageById(int id)
        {
            return await _db.ChatMessages.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> SaveMessage(ChatMessage entity)
        {
            _db.ChatMessages.Add(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Chat> Update(Chat entity)
        {
            _db.Chats.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<ChatMessage> UpdateMessage(ChatMessage entity)
        {
            _db.ChatMessages.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
