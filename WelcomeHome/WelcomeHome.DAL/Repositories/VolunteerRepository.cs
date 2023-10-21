using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public sealed class VolunteerRepository : IVolunteerRepository
{
	private readonly WelcomeHomeDbContext _context;

	public VolunteerRepository(WelcomeHomeDbContext context)
	{
		_context = context;
	}

	public IEnumerable<Volunteer> GetAll()
	{
		return _context.Volunteers
			           .Include(v => v.Establishment)
			           .AsNoTracking()
			           .Select(v => v);
	}

	public async Task<Volunteer> GetByIdAsync(int id)
	{
		return await _context.Volunteers
			                 .Include(v => v.Establishment)
							 .AsNoTracking()
			                 .SingleAsync(v => v.Id == id)
			                 .ConfigureAwait(false);
	}

	public async Task AddAsync(Volunteer volunteer)
	{
		await _context.Volunteers.AddAsync(volunteer).ConfigureAwait(false);
		await AttachEstablishmentAsync(volunteer).ConfigureAwait(false);

		await _context.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task UpdateAsync(Volunteer volunteer)
	{
		var foundVolunteer = await _context.Volunteers
			                               .SingleAsync(v => v.Id == volunteer.Id)
			                               .ConfigureAwait(false);

		foundVolunteer.PasswordHash = volunteer.PasswordHash;
		foundVolunteer.PasswordSalt = volunteer.PasswordSalt;
		foundVolunteer.FullName = volunteer.FullName;
		foundVolunteer.PhoneNumber = volunteer.PhoneNumber;
		foundVolunteer.Email = volunteer.Email;
		foundVolunteer.Telegram = volunteer.Telegram;
		foundVolunteer.Document = volunteer.Document;
		foundVolunteer.EstablishmentId = volunteer.EstablishmentId;
		foundVolunteer.ContractId = volunteer.ContractId;

		_context.Volunteers.Update(foundVolunteer);
		await AttachEstablishmentAsync(foundVolunteer).ConfigureAwait(false);

		await _context.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task DeleteAsync(int id)
	{
		var foundVolunteer = await _context.Volunteers.SingleAsync(v => v.Id == id).ConfigureAwait(false);

		_context.Volunteers.Remove(foundVolunteer);
		await _context.SaveChangesAsync().ConfigureAwait(false);
	}

	private async Task AttachEstablishmentAsync(Volunteer volunteer)
	{
		if (volunteer.EstablishmentId != null)
		{
			var foundEstablishment = await _context.Establishments
				                                   .SingleAsync(e => e.Id == volunteer.EstablishmentId)
				                                   .ConfigureAwait(false);

			_context.Establishments.Attach(foundEstablishment);
			_context.Entry(foundEstablishment).State = EntityState.Unchanged;
		}
	}
}
