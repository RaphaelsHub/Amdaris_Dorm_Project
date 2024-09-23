using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Entities.Laundry;
using Dorm.Domain.Entities.UserEF;
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

        public async Task<IEnumerable<Reservation>> HasReservation(int washerId, DateTime startTime, DateTime endTime)
        {
            return await _db.Reservations.Where(r => r.WasherId == washerId && startTime <= r.EndTime && endTime >= r.StartTime).ToListAsync();
        }

        public async Task<bool> Delete(Reservation entity)
        {
            _db.Reservations.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Reservation>> GetAll()
        {
            return await _db.Reservations.ToListAsync();
        }

        public async Task<Reservation?> GetById(int WasherId)
        {
            return await _db.Reservations.FirstOrDefaultAsync(x => x.Id == WasherId);
        }

        public async Task<Reservation> Update(Reservation entity)
        {
            _db.Reservations.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Reservation>> GetAllByUserId(int userId)
        {
            return await _db.Reservations.Where(r => userId == r.UserId).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllByWasherId(int washerId)
        {
            return await _db.Reservations.Where(r => washerId == r.WasherId).ToListAsync();
        }
    }
}
