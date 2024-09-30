namespace Dorm.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Delete(T entity);
        Task<T> Update(T entity);
    }
}
