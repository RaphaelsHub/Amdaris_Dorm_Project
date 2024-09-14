using Dorm.DAL.Interfaces;
using Dorm.Domain.Entities.Laundry;
using Dorm.Domain.Responces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.DAL.Repositories
{
    public class WasherRepository : IWasherRepository
    {
        private readonly ApplicationDbContext _db;
        public WasherRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Reservation entity)
        {
            _db.Reservations.Add(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(Reservation entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Reservation>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Reservation?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Reservation>> HasReservation(int washerId, DateTime startTime, DateTime endTime)
        {
            return await _db.Reservations.Where(r => startTime <= r.EndTime && endTime >= r.StartTime).ToListAsync();
        }

        public Task<Reservation> Update(Reservation entity)
        {
            throw new NotImplementedException();
        }

        /*public async Task<bool> Delete(Washer entity)
        {
            _db.Reservations.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Washer>> GetAll()
        {
            return await _db.Reservations.ToListAsync();
        }

        public async Task<Washer?> GetById(int WasherId)
        {
            return await _db.Reservations.FirstOrDefaultAsync(x => x.Id == WasherId);
        }

        public async Task<Washer> Update(Washer entity)
        {
            _db.Reservations.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }*/
    }
}
