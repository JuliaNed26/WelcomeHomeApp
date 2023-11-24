﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

	public async Task DeleteForUserAsync(Guid userId)
	{
		var foundRefreshToken = await _context.RefreshTokens
			                                  .SingleOrDefaultAsync(rt => rt.UserId == userId)
			                                  .ConfigureAwait(false)
							    ?? throw new NotFoundException("Refresh token was not found");

		_context.RefreshTokens.Remove(foundRefreshToken);
		await _context.SaveChangesAsync().ConfigureAwait(false);
	}
}