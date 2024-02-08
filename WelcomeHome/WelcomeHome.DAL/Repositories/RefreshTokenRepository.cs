using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Exceptions;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly WelcomeHomeDbContext _context;

    public RefreshTokenRepository(WelcomeHomeDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        var foundRefreshToken = await _context.RefreshTokens
                                              .Include(rt => rt.User)
                                              .AsNoTracking()
                                              .SingleOrDefaultAsync(rt => rt.Token == token)
                                              .ConfigureAwait(false)
                                ?? throw new NotFoundException("Refresh token was not found");
        return foundRefreshToken;
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task UpdateAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task DeleteForUserAsync(long userId)
    {
        var foundRefreshToken = await _context.RefreshTokens
                                              .SingleOrDefaultAsync(rt => rt.UserId == userId)
                                              .ConfigureAwait(false);
        if (foundRefreshToken != null)
        {
            _context.RefreshTokens.Remove(foundRefreshToken);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    public async Task DeleteAllForUserAsync(long userId)
    {
        var allTokens = await _context.RefreshTokens.ToListAsync();

        var userRefreshTokens = allTokens.Where(rt => rt.UserId == userId);

        if (userRefreshTokens.Any())
        {
            _context.RefreshTokens.RemoveRange(userRefreshTokens);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}