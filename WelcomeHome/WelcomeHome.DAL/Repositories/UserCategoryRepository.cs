using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public sealed class UserCategoryRepository : IUserCategoryRepository
    {
        private readonly WelcomeHomeDbContext _context;
        public UserCategoryRepository(WelcomeHomeDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserCategory userCategory)
        {
            await _context.UserCategories.AddAsync(userCategory).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            var foundUserCategory = await _context.UserCategories.SingleAsync(u => u.Id == id).ConfigureAwait(false);

            _context.UserCategories.Remove(foundUserCategory);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public IEnumerable<UserCategory> GetAll()
        {
            return _context.UserCategories
                .Include(u => u.SocialPayments)
                .AsNoTracking()
                .Select(u => u);
        }

        public async Task<UserCategory?> GetByIdAsync(Guid id)
        {
            return await _context.UserCategories
                .Include(u => u.SocialPayments)
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id)
                .ConfigureAwait(false);

        }

        public async Task UpdateAsync(UserCategory userCategory)
        {
            var foundUserCategory = await _context.UserCategories
                                         .SingleAsync(u => u.Id == userCategory.Id)
                                         .ConfigureAwait(false);

            foundUserCategory.Name = foundUserCategory.Name;
            foundUserCategory.SocialPayments = userCategory.SocialPayments;
            _context.UserCategories.Update(foundUserCategory);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
