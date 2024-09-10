using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO;
using Dorm.Domain.Entities.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.DAL.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _db;
        public TicketRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<bool> Create(Ticket entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Ticket entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ticket>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Ticket> GetById(int ticketId)
        {
            return new Ticket();
        }

        public Task<Ticket> Update(Ticket entity)
        {
            throw new NotImplementedException();
        }
    }
}
