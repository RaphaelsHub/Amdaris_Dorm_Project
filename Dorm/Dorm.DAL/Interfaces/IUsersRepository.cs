namespace Dorm.DAL.Interfaces
{
    public interface IUsersRepository<UserEF> : IBaseRepository<UserEF>
    {
        Task<UserEF?> GetByEmail(string email);
    }
}
