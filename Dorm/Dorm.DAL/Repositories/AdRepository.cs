using Dorm.DAL.Interfaces;
using Dorm.Domain.Entities.Ad;
using Dorm.Domain.Entities.Ticket;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.DAL.Repositories
{
    public class AdRepository : IAdRepository
    {
        private readonly ApplicationDbContext _db;
        public AdRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Ad entity)
        {
            _db.Ads.Add(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Ad entity)
        {
            _db.Ads.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Ad>> GetAll()
        {
            return await _db.Ads.ToListAsync();
        }

        public async Task<Ad?> GetById(int id)
        {
            return await _db.Ads.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Ad> Update(Ad entity)
        {
            _db.Ads.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
