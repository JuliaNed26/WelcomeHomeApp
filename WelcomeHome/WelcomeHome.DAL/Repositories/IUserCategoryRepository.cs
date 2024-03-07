using WelcomeHome.DAL.Models;


namespace WelcomeHome.DAL.Repositories
{
    public interface IUserCategoryRepository
    {
        Task<UserCategory?> GetByIdAsync(long id);

        IEnumerable<UserCategory> GetAll();

        Task AddAsync(UserCategory userCategory);

        Task UpdateAsync(UserCategory userCategory);

        Task DeleteAsync(long id);
    }
}
