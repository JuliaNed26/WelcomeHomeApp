using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Exceptions;
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

	public async Task<Volunteer?> GetByIdAsync(Guid id)
	{
		return await _context.Volunteers
			                 .Include(v => v.Establishment)
							 .AsNoTracking()
			                 .SingleOrDefaultAsync(v => v.UserId == id)
			                 .ConfigureAwait(false);
	}

	public async Task AddAsync(Volunteer volunteer)
	{
		await _context.Volunteers.AddAsync(volunteer).ConfigureAwait(false);
		AttachEstablishment(volunteer);

		await _context.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task UpdateAsync(Volunteer volunteer)
	{
		AttachEstablishment(volunteer);
		_context.Volunteers.Update(volunteer);

		await _context.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task DeleteAsync(Guid id)
	{
		var foundVolunteer = await _context.Volunteers
			                               .FindAsync(id)
			                               .ConfigureAwait(false)
		                     ?? throw new NotFoundException($"Volunteer with Id {id} not found for deletion.");

		_context.Volunteers.Remove(foundVolunteer);
		await _context.SaveChangesAsync().ConfigureAwait(false);
	}

	private void AttachEstablishment(Volunteer volunteer)
	{
		if (volunteer.Establishment != null)
		{
			_context.Establishments.Attach(volunteer.Establishment);
			_context.Entry(volunteer.Establishment).State = EntityState.Unchanged;
		}
	}
}
