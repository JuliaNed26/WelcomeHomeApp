using WelcomeHome.DAL.Models;


namespace WelcomeHome.DAL.Repositories
{
    public interface IUserCategoryRepository
    {
        Task<UserCategory?> GetByIdAsync(int id);

        IEnumerable<UserCategory> GetAll();

        Task AddAsync(UserCategory userCategory);

        Task UpdateAsync(UserCategory userCategory);

        Task DeleteAsync(int id);
    }
}
