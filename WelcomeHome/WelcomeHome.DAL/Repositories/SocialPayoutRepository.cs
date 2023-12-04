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
                         .Include(p => p.UserCategories)
                         .Select(sp => sp);
    }

    public async Task<SocialPayout?> GetByIdAsync(int id)
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
        if (socialPayout.UserCategories != null)
        {
            AttachUserCategories(socialPayout.UserCategories);
        }

        await _dbContext.SocialPayouts.AddAsync(socialPayout).ConfigureAwait(false);

        if (steps != null)
        {
            AddStepsToSocialPayout(steps, socialPayout);
        }

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task UpdateWithStepsAsync(SocialPayout socialPayout, Dictionary<int, Step> steps)
    {
        try
        {
            if (socialPayout.UserCategories != null)
            {
                AttachUserCategories(socialPayout.UserCategories);
            }

            if (steps != null)
            {
                AddStepsToSocialPayout(steps, socialPayout);
            }
            _dbContext.SocialPayouts.Update(socialPayout);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public async Task DeleteAsync(int socialPayoutId)
    {
        var socialPayout = await _dbContext.SocialPayouts.FindAsync(socialPayoutId)
                           ?? throw new NotFoundException($"SocialPayout with Id {socialPayoutId} not found for delete.");

        _dbContext.SocialPayouts.Remove(socialPayout);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }


    private void AddStepsToSocialPayout(Dictionary<int, Step> steps, SocialPayout socialPayout)
    {
        _dbContext.Steps.AddRange(steps.Values);
        var newPaymentSteps = GeneratePaymentStep(socialPayout.Id, steps);

        if (socialPayout.PaymentSteps == null)
        {
            socialPayout.PaymentSteps = GeneratePaymentStep(socialPayout.Id, steps);
        }
        else
        {
            socialPayout.PaymentSteps.AddRange(newPaymentSteps);
        }
    }


    private List<PaymentStep> GeneratePaymentStep(int SocialPayoutId, Dictionary<int, Step> steps)
    {
        List<PaymentStep> paymentSteps = new List<PaymentStep>();
        foreach (var step in steps)
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