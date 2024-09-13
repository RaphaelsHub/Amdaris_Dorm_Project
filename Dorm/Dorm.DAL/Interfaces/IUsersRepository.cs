namespace Dorm.DAL.Interfaces
{
    public interface IUsersRepository<T>
    {
        Task<T> Create(T entity);
        Task<T?> Update(T entity);
        Task<T?> GetById(int id);
        Task<T?> GetByEmail(string email);
    }
}
