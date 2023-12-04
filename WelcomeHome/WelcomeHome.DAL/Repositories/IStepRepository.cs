using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface IStepRepository
{
	IEnumerable<Step> GetAll();

	Task<Step?> GetByIdAsync(int id);

	Task AddAsync(Step step);

	Task UpdateAsync(Step step);

	Task DeleteAsync(int id);

	Task<Step?> GetByEstablishmentTypeAndDocuments(int establishmentTypeId, ICollection<int> documentsRecieveIds, ICollection<int> documentsBringIds);
}
