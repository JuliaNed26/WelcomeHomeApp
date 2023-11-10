using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Exceptions;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public sealed class UserRepository : IUserRepository
{
	private readonly WelcomeHomeDbContext _context;

	public UserRepository(WelcomeHomeDbContext context)
	{
		_context = context;
	}

	public IEnumerable<User> GetAll()
	{
		return _context.Users
			           .AsNoTracking()
			           .Select(u => u);
	}

	public async Task<User?> GetByIdAsync(Guid id)
	{
		return await _context.Users
			                 .AsNoTracking()
			                 .SingleOrDefaultAsync(u => u.Id == id)
			                 .ConfigureAwait(false);
	}

	public async Task AddAsync(User user)
	{
		await _context.Users.AddAsync(user).ConfigureAwait(false);
		await _context.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task UpdateAsync(User user)
	{
		_context.Users.Update(user);
		await _context.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task DeleteAsync(Guid id)
	{
		var foundUser = await _context.Users
			                          .FindAsync(id)
			                          .ConfigureAwait(false)
		                ?? throw new NotFoundException($"User with Id {id} not found for deletion.");

		_context.Users.Remove(foundUser);
		await _context.SaveChangesAsync().ConfigureAwait(false);
	}
}
