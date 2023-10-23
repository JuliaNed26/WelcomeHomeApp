using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface IUserRepository
{
	IEnumerable<User> GetAll();

	Task<User?> GetByIdAsync(Guid id);

	Task AddAsync(User user);

	Task UpdateAsync(User user);

	Task DeleteAsync(Guid id);
}
