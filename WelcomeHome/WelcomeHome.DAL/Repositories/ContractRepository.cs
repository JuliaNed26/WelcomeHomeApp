using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public sealed class ContractRepository : IContractRepository
{
	private readonly WelcomeHomeDbContext _context;

	public ContractRepository(WelcomeHomeDbContext context)
	{
		_context = context;
	}

	public IEnumerable<Contract> GetAll()
	{
		return _context.Contracts
			           .Include(c => c.Volunteer)
			           .AsNoTracking()
			           .Select(c => c);
	}

	public async Task<Contract> GetByIdAsync(int id)
	{
		return await _context.Contracts
			                 .Include(c => c.Volunteer)
			                 .AsNoTracking()
			                 .SingleAsync(c => c.Id == id)
			                 .ConfigureAwait(false);
	}

	public async Task AddAsync(Contract contract)
	{
		await _context.Contracts.AddAsync(contract).ConfigureAwait(false);
		await AttachVolunteerAsync(contract).ConfigureAwait(false);

		await _context.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task UpdateAsync(Contract contract)
	{
		var foundContract = await _context.Contracts
			                              .SingleAsync(c => c.Id == contract.Id)
			                              .ConfigureAwait(false);

		foundContract.DateStart = contract.DateStart;
		foundContract.DateEnd = contract.DateEnd;
		foundContract.URL = contract.URL;

		_context.Contracts.Update(foundContract);
		await AttachVolunteerAsync(contract).ConfigureAwait(false);

		await _context.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task DeleteAsync(int id)
	{
		var foundContract = await _context.Contracts.SingleAsync(c => c.Id == id).ConfigureAwait(false);

		_context.Contracts.Remove(foundContract);
		await _context.SaveChangesAsync().ConfigureAwait(false);
	}

	private async Task AttachVolunteerAsync(Contract contract)
	{
		var foundVolunteer = await _context.Volunteers
			                               .SingleAsync(v => v.Id == contract.VolunteerId)
			                               .ConfigureAwait(false);

		_context.Volunteers.Attach(foundVolunteer);
		_context.Entry(foundVolunteer).State = EntityState.Unchanged;
	}
}
