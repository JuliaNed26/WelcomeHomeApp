using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface IDocumentRepository
{
	IEnumerable<Document> GetAll();

	Task<Document?> GetAsync(Guid id);

	Task AddAsync(Document document);

	Task UpdateAsync(Document document);

	Task DeleteAsync(Guid id);
}
