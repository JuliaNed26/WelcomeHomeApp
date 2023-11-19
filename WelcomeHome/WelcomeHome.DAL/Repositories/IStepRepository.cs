using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface IStepRepository
{
	IEnumerable<Step> GetAll();

	Task<Step?> GetByIdAsync(Guid id);

	Task AddAsync(Step step);

	Task UpdateAsync(Step step);

	Task DeleteAsync(Guid id);

	Task<Step?> GetByEstablishmentTypeAndDocuments(Guid establishmentTypeId, ICollection<Guid> documentsRecieveIds, ICollection<Guid> documentsBringIds);
}
