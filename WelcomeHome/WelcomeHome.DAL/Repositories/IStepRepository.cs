using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface IStepRepository
{
	IEnumerable<Step> GetAll();

	Task<Step?> GetByIdAsync(long id);

	Task AddAsync(Step step);

	Task UpdateAsync(Step step);

	Task DeleteAsync(long id);

	Task<Step?> GetByEstablishmentTypeAndDocuments(long establishmentTypeId, ICollection<long> documentsRecieveIds, ICollection<long> documentsBringIds);
}
