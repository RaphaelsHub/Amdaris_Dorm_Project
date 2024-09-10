using Dorm.DAL.Interfaces;
using Dorm.Domain.Entities.Ticket;
using Microsoft.EntityFrameworkCore;

namespace Dorm.DAL.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _db;
        public TicketRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Ticket entity)
        {
            _db.Tickets.Add(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Ticket entity)
        {
            _db.Tickets.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _db.Tickets.ToListAsync();
        }

        public async Task<Ticket?> GetById(int ticketId)
        {
            return await _db.Tickets.FirstOrDefaultAsync(x => x.Id == ticketId);
        }

        public async Task<Ticket> Update(Ticket entity)
        {
            _db.Tickets.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
