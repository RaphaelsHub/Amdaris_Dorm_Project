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

        public async Task<UserEF> Create(UserEF entity)
        {
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();

            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == entity.Email);

            return user!;
        }

        public async Task<UserEF?> GetByEmail(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<UserEF?> GetById(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<UserEF?> Update(UserEF entity)
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();

            var user = _db.Users.FirstOrDefault(entity => entity.Email == entity.Email);
            return user;
        }
    }
}

