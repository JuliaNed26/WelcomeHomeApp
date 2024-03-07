using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Exceptions;
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

        public async Task DeleteAsync(long id)
        {
            var foundUserCategory = await _context.UserCategories
	                                              .FindAsync(id)
	                                              .ConfigureAwait(false)
                                    ?? throw new NotFoundException($"User category with Id {id} not found for deletion.");

            _context.UserCategories.Remove(foundUserCategory);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public IEnumerable<UserCategory> GetAll()
        {
            return _context.UserCategories
                .Include(u => u.SocialPayouts)
                .AsNoTracking()
                .Select(u => u);
        }

        public async Task<UserCategory?> GetByIdAsync(long id)
        {
            return await _context.UserCategories
                .Include(u => u.SocialPayouts)
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id)
                .ConfigureAwait(false);

        }

        public async Task UpdateAsync(UserCategory userCategory)
        {
            if (userCategory.Id == 0)
            {
                throw new NotFoundException($"User category with id {userCategory.Id} was not found");
            }
            _context.UserCategories.Update(userCategory);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
