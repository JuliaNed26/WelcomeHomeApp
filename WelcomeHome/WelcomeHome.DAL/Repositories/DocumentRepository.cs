using WelcomeHome.DAL.Exceptions;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public sealed class DocumentRepository : IDocumentRepository
{
	private readonly WelcomeHomeDbContext _dbContext;

	public DocumentRepository(WelcomeHomeDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public IEnumerable<Document> GetAll()
	{
		return _dbContext.Documents.Select(d => d);
	}

	public async Task<Document?> GetByIdAsync(Guid id)
	{
		return await _dbContext.Documents.FindAsync(id).ConfigureAwait(false);
	}

	public async Task AddAsync(Document document)
	{
		document.Id = Guid.NewGuid();

		await _dbContext.Documents.AddAsync(document).ConfigureAwait(false);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(Document document)
	{
		_dbContext.Documents.Update(document);
		await _dbContext.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task DeleteAsync(Guid id)
	{
		var document = await _dbContext.Documents.FindAsync(id).ConfigureAwait(false)
					   ?? throw new NotFoundException($"Document with Id {id} not found for deletion.");

		_dbContext.Documents.Remove(document);
		await _dbContext.SaveChangesAsync().ConfigureAwait(false);
	}
}
