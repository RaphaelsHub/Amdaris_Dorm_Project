using Dorm.Domain.Entities.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.DAL.Interfaces
{
    public interface ITicketRepository : IBaseRepository<Ticket>
    {
        Task<bool> Delete(Ticket ticket);
        Task<Ticket> GetById(int id);
        Task<Ticket> Update(Ticket entity);
    }
}
