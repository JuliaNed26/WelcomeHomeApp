using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface IUserRepository
{
	IEnumerable<User> GetAll();

	Task<User?> GetByIdAsync(int id);

	Task AddAsync(User user);

	Task UpdateAsync(User user);

	Task DeleteAsync(int id);
}
