﻿using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public sealed class CountryRepository : ICountryRepository
{
	private readonly WelcomeHomeDbContext _context;

	public CountryRepository(WelcomeHomeDbContext context)
	{
		_context = context;
	}

	public IEnumerable<Country> GetAll()
	{
		return _context.Countries
			           .Include(c => c.Cities)
			           .AsNoTracking()
			           .Select(c => c);
	}

	public async Task<Country> GetByIdAsync(int id)
	{
		return await _context.Countries
			                 .Include(c => c.Cities)
			                 .AsNoTracking()
			                 .SingleAsync(c => c.Id == id)
			                 .ConfigureAwait(false);
	}

	public async Task AddAsync(Country country)
	{
		await _context.Countries.AddAsync(country).ConfigureAwait(false);
		await _context.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task UpdateAsync(Country country)
	{
		var foundCountry = await _context.Countries
			                             .SingleAsync(c => c.Id == country.Id)
			                             .ConfigureAwait(false);

		foundCountry.Name = country.Name;

		_context.Countries.Update(foundCountry);
		await _context.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task DeleteAsync(int id)
	{
		var foundCountry = await _context.Countries.SingleAsync(c => c.Id == id).ConfigureAwait(false);

		_context.Countries.Remove(foundCountry);
		await _context.SaveChangesAsync().ConfigureAwait(false);
	}
}