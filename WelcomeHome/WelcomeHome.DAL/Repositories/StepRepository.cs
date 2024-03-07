using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Exceptions;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public sealed class StepRepository : IStepRepository
{
	private readonly WelcomeHomeDbContext _dbContext;

	public StepRepository(WelcomeHomeDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public IEnumerable<Step> GetAll()
	{
		return _dbContext.Steps
			             .AsNoTracking()
						 .Include(s => s.StepDocuments)
			             .ThenInclude(sd => sd.Document)
			             .Select(s => s);
	}

	public async Task<Step?> GetByIdAsync(long id)
	{
		return await _dbContext.Steps
			                   .AsNoTracking()
							   .Include(s => s.StepDocuments)
			                   .ThenInclude(sd => sd.Document)
			                   .FirstOrDefaultAsync(s => s.Id == id)
			                   .ConfigureAwait(false);
	}

	public async Task AddAsync(Step step)
	{
		await _dbContext.Steps.AddAsync(step).ConfigureAwait(false);
		await _dbContext.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task UpdateAsync(Step step)
    {
        if (step.Id == 0)
        {
            throw new NotFoundException($"Step with id {step.Id} was not found");
        }
        _dbContext.Steps.Update(step);
		await _dbContext.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task DeleteAsync(long id)
	{
		var step = await _dbContext.Steps.FindAsync(id).ConfigureAwait(false)
				   ?? throw new NotFoundException($"Step with Id {id} not found for deletion.");

		_dbContext.Steps.Remove(step);
		await _dbContext.SaveChangesAsync().ConfigureAwait(false);
	}

	
	public async Task<Step?> GetByEstablishmentTypeAndDocuments(long establishmentTypeId, ICollection<long> documentsRecieveIds, ICollection<long> documentsBringIds)
	{
		var step = await _dbContext.Steps
			.Where(s =>
					s.EstablishmentTypeId == establishmentTypeId &&
					!s.StepDocuments.Any(sd => sd.ToReceive == true && !documentsRecieveIds.Contains(sd.DocumentId)) &&
					!s.StepDocuments.Any(sd => sd.ToReceive == false && !documentsBringIds.Contains(sd.DocumentId)) 
				   ).FirstOrDefaultAsync();

		return step;
	}
	
}
