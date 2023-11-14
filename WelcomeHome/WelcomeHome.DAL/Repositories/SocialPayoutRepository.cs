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

	public async Task<SocialPayout?> GetByIdAsync(Guid id)
	{
		return await _dbContext.SocialPayouts
			                   .AsNoTracking()
							   .Include(p => p.PaymentSteps)
			                   .ThenInclude(ps => ps.Step)
			                   .FirstOrDefaultAsync(p => p.Id == id)
			                   .ConfigureAwait(false);
	}

	public async Task AddWithStepsAsync(SocialPayout socialPayout, Dictionary<int, Step> steps)
	{
		try
		{
            socialPayout.Id = Guid.NewGuid();

            AttachUserCategories(socialPayout.UserCategories);

            await _dbContext.SocialPayouts.AddAsync(socialPayout).ConfigureAwait(false);

            AddStepsToSicialPayout(steps, socialPayout);

			await _dbContext.SaveChangesAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}
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


    private void AddStepsToSicialPayout(Dictionary<int, Step> steps, SocialPayout socialPayout)
    {
		GenerateGuidForSteps(steps.Values);
		_dbContext.Steps.AddRange(steps.Values);
        socialPayout.PaymentSteps = GeneratePaymentStep(socialPayout.Id, steps);
    }


	private void GenerateGuidForSteps(ICollection<Step> steps)
	{
        foreach (var step in steps)
        {
            step.Id = Guid.NewGuid();
        }
    }

    private List<PaymentStep> GeneratePaymentStep(Guid SocialPayoutId, Dictionary<int, Step> steps)
	{
		List<PaymentStep> paymentSteps = new List<PaymentStep>();
		foreach(var step in steps)
		{
			var newPaymentStep = new PaymentStep
			{
				SocialPayoutId = SocialPayoutId,
				Step = step.Value,
				SequenceNumber = step.Key
			};
			paymentSteps.Add(newPaymentStep);
		}

		return paymentSteps;
	}

	private void AttachUserCategories(ICollection<UserCategory> categories)
	{
		foreach (var c in categories)
		{
			_dbContext.UserCategories.Attach(c);
			if (c.SocialPayouts != null)
			{
				foreach (var s in c.SocialPayouts)
					DettachSocialPayout(s);
			}
			_dbContext.Entry(c).State = EntityState.Unchanged;
		}
	}

	private void DettachSocialPayout(SocialPayout socialPayout)
	{
		_dbContext.Entry(socialPayout).State = EntityState.Detached;
	}

	private void AttachStep(Step step)
	{
        _dbContext.Steps.Attach(step);
        _dbContext.Entry(step).State = EntityState.Unchanged;
    }




	
	

}
