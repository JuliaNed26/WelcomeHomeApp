using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Exceptions;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public class SocialPayoutRepository : ISocialPayoutRepository
{
	private readonly WelcomeHomeDbContext _dbContext;

	public SocialPayoutRepository(WelcomeHomeDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public IEnumerable<SocialPayout> GetAll()
	{
		return _dbContext.SocialPayouts
			             .AsNoTracking()
						 .Include(p => p.PaymentSteps)
			             .ThenInclude(ps => ps.Step)
			             .Select(sp => sp);
	}

	public async Task<SocialPayout?> GetAsync(Guid id)
	{
		return await _dbContext.SocialPayouts
			                   .AsNoTracking()
							   .Include(p => p.PaymentSteps)
			                   .ThenInclude(ps => ps.Step)
			                   .FirstOrDefaultAsync(p => p.Id == id)
			                   .ConfigureAwait(false);
	}

	public async Task AddWithStepsAsync(SocialPayout socialPayout, IEnumerable<Guid> stepIds)
	{
		socialPayout.Id = Guid.NewGuid();

		await _dbContext.SocialPayouts.AddAsync(socialPayout).ConfigureAwait(false);
		await _dbContext.SaveChangesAsync().ConfigureAwait(false);

	}
	public async Task UpdateWithStepsAsync(SocialPayout socialPayout, IEnumerable<Guid> stepIds)
	{
		_dbContext.SocialPayouts.Update(socialPayout);
		await _dbContext.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task DeleteAsync(Guid socialPayoutId)
	{
		var socialPayout = await _dbContext.SocialPayouts.FindAsync(socialPayoutId) 
		                   ?? throw new NotFoundException($"SocialPayout with Id {socialPayoutId} not found for delete.");

		_dbContext.SocialPayouts.Remove(socialPayout);
		await _dbContext.SaveChangesAsync().ConfigureAwait(false);
	}
}
