using Dorm.DAL.Interfaces;
using Dorm.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dorm.DAL.Repositories
{
    public class UsersRepository : IUsersRepository<User>
    {
        private readonly ApplicationDbContext _db;

        public UsersRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User> Create(User entity)
        {
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();

            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == entity.Email);

            return user!;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<User?> GetById(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<User> Update(User entity)
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();

            var user = _db.Users.FirstOrDefault(entity => entity.Email == entity.Email);
            return user;
        }
    }
}

