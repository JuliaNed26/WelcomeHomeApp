using WelcomeHome.DAL.Models;


namespace WelcomeHome.DAL.Repositories
{
    public interface IUserCategoryRepository
    {
        Task<UserCategory?> GetByIdAsync(Guid id);

        IEnumerable<UserCategory> GetAll();

        Task AddAsync(UserCategory userCategory);

        Task UpdateAsync(UserCategory userCategory);

        Task DeleteAsync(Guid id);
    }
}
