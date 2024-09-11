using Dorm.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.DAL.Interfaces
{
    public interface IUsersRepository<T>
    {
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T?> GetById(int id);
        Task<T?> GetByEmail(string email);
    }
}
