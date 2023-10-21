using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface IContractRepository
{
	Task<Contract> GetByIdAsync(int id);

	IEnumerable<Contract> GetAll();

	Task AddAsync(Contract contract);

	Task UpdateAsync(Contract contract);

	Task DeleteAsync(int id);
}
