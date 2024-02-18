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
                               .Include(p => p.UserCategories)
                               .FirstOrDefaultAsync(p => p.Id == id)
                               .ConfigureAwait(false);
    }

    public async Task AddAsync(SocialPayout socialPayout)
    {
        if (socialPayout.UserCategories != null)
        {
            AttachUserCategories(socialPayout.UserCategories);
        }

        await _dbContext.SocialPayouts.AddAsync(socialPayout).ConfigureAwait(false);


        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task UpdateAsync(SocialPayout socialPayout)
    {
        _dbContext.SocialPayouts.Update(socialPayout);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task DeleteAsync(int socialPayoutId)
    {
        var socialPayout = await _dbContext.SocialPayouts.FindAsync(socialPayoutId)
                           ?? throw new NotFoundException($"SocialPayout with Id {socialPayoutId} not found for delete.");

        _dbContext.SocialPayouts.Remove(socialPayout);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
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