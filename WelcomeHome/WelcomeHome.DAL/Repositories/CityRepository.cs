using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public sealed class CityRepository : ICityRepository
{
    private readonly WelcomeHomeDbContext _context;

    public CityRepository(WelcomeHomeDbContext context)
    {
        _context = context;
    }

    public IEnumerable<City> GetAll()
    {
        return _context.Cities
                       .Include(c => c.Country)
                       .AsNoTracking()
                       .Select(c => c);
    }

    public async Task<City?> GetByIdAsync(int id)
    {
        return await _context.Cities
                             .Include(c => c.Country)
                             .AsNoTracking()
                             .SingleOrDefaultAsync(c => c.Id == id)
                             .ConfigureAwait(false);
    }

    public async Task AddAsync(City city)
    {
        await _context.Cities.AddAsync(city).ConfigureAwait(false);
        await AttachCountryAsync(city).ConfigureAwait(false);

        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task UpdateAsync(City city)
    {
        await AttachCountryAsync(city).ConfigureAwait(false);
        _context.Cities.Update(city);

        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task DeleteAsync(int id)
    {
        var foundCity = await _context.Cities.SingleAsync(c => c.Id == id).ConfigureAwait(false);

        _context.Cities.Remove(foundCity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    private async Task AttachCountryAsync(City city)
    {
        var foundCountry = await _context.Countries
                                         .SingleAsync(c => c.Id == city.CountryId)
                                         .ConfigureAwait(false);

        _context.Countries.Attach(foundCountry);
        _context.Entry(foundCountry).State = EntityState.Unchanged;
    }
}
