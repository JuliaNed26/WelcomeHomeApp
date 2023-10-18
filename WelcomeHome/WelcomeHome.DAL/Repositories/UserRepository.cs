using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public sealed class UserRepository : IUserRepository
{
	private WelcomeHomeDbContext _context;

	public UserRepository(WelcomeHomeDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<User>> GetAllAsync()
	{
		return await _context.Users
			                 .AsNoTracking()
			                 .ToListAsync()
			                 .ConfigureAwait(false);
	}

	public async Task<User?> GetByIdAsync(int id)
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
		var foundUser = await _context.Users
			                          .SingleAsync(u => u.Id == user.Id)
			                          .ConfigureAwait(false);

		foundUser.Email = user.Email;
		foundUser.FullName = user.FullName;
		foundUser.UserName = user.UserName;
		foundUser.PhoneNumber = user.PhoneNumber;
		foundUser.PasswordHash = user.PasswordHash;
		foundUser.PasswordSalt = user.PasswordSalt;

		_context.Users.Update(foundUser);
		await _context.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task DeleteAsync(int id)
	{
		var foundUser = await _context.Users.SingleAsync(u => u.Id == id).ConfigureAwait(false);

		_context.Users.Remove(foundUser);
		await _context.SaveChangesAsync().ConfigureAwait(false);
	}
}
