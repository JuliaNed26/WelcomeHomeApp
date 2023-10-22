using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public class SocialPaymentRepository : ISocialPaymentRepository
    {
        private WelcomeHomeDbContext _context;

        public SocialPaymentRepository(WelcomeHomeDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<SocialPayment>> GetAllAsync()
        {
            return _context.SocialPayments.Include(sp => sp.Establishment)
                                          .Include(sp => sp.UserCategory)
                                          .AsNoTracking()
                                          .Select(sp => sp);
        }

        public async Task<SocialPayment?> GetByIdAsync(Guid id)
        {
            return await _context.SocialPayments.Include(sp => sp.Establishment)
                                        .Include(sp => sp.UserCategory)
                                        .FirstOrDefaultAsync(sp => sp.Id == id)
                                        .ConfigureAwait(false);
        }

        public async Task Add(SocialPayment newSocialPayment)
        {

            await _context.SocialPayments.AddAsync(newSocialPayment).ConfigureAwait(false);
            await AttachEstablishmentAsync(newSocialPayment).ConfigureAwait(false);
            await AttachUserCategoryAsync(newSocialPayment).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Delete(Guid id)
        {
            var existingSocialPayment = await _context.SocialPayments.SingleAsync(sp => sp.Id == id).ConfigureAwait(false);
            _context.SocialPayments.Remove(existingSocialPayment);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Update(SocialPayment editedSocialPayment)
        {
            var existingSocialPayment = await _context.SocialPayments.SingleAsync(e => e.Id == editedSocialPayment.Id).ConfigureAwait(false);

            existingSocialPayment.Name = editedSocialPayment.Name;
            existingSocialPayment.Amount = editedSocialPayment.Amount;
            existingSocialPayment.Description = editedSocialPayment.Description;
            existingSocialPayment.EstablishmentId = editedSocialPayment.EstablishmentId;
            existingSocialPayment.Documents = editedSocialPayment.Documents;
            existingSocialPayment.UserCategoryId = editedSocialPayment.UserCategoryId;

            _context.SocialPayments.Update(existingSocialPayment);

            await AttachEstablishmentAsync(editedSocialPayment).ConfigureAwait(false);
            await AttachUserCategoryAsync(editedSocialPayment).ConfigureAwait(false);


            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task AttachEstablishmentAsync(SocialPayment socialPayment)
        {
            var existingEstablishment = await _context.Establishments.SingleAsync(e => e.Id == socialPayment.EstablishmentId).ConfigureAwait(false);

            _context.Establishments.Attach(existingEstablishment);
            _context.Entry(existingEstablishment).State = EntityState.Unchanged;
        }

        private async Task AttachUserCategoryAsync(SocialPayment socialPayment)
        {
            var existingUserCategory = await _context.UserCategories.SingleAsync(uc => uc.Id == socialPayment.UserCategoryId).ConfigureAwait(false);

            _context.UserCategories.Attach(existingUserCategory);
            _context.Entry(existingUserCategory).State = EntityState.Unchanged;
        }
    }
}
