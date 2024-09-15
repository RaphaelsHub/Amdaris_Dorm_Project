using Dorm.DAL.Interfaces;
using Dorm.Domain.Entities.UserEF;
using Microsoft.EntityFrameworkCore;

namespace Dorm.DAL.Repositories
{
    public class UsersRepository : IUsersRepository<UserEF>
    {
        private readonly ApplicationDbContext _db;

        public UsersRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(UserEF entity)
        {
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();

            return (await _db.Users.FirstOrDefaultAsync(x => x.Email == entity.Email)) != null;
        }

        public async Task<UserEF?> GetByEmail(string email) => await _db.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<UserEF?> GetById(int id) => await _db.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<UserEF?> Update(UserEF entity)
        {
             _db.Users.Update(entity);
            await _db.SaveChangesAsync();
            
            return _db.Users.FirstOrDefault(entity => entity.Email == entity.Email);
        }

        public async Task<bool> Delete(UserEF entity)
        {
            _db.Users.Remove(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<UserEF>> GetAll() => await _db.Users.ToListAsync();

    }
}

