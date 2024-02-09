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
                       .Include(v => v.Organization)
                       .Include(v => v.User)
                       .AsNoTracking()
                       .Select(v => v);
    }

    public async Task<Volunteer?> GetByIdAsync(long id)
    {
        return await _context.Volunteers
                             .Include(v => v.Organization)
                             .Include(v => v.User)
                             .AsNoTracking()
                             .SingleOrDefaultAsync(v => v.UserId == id)
                             .ConfigureAwait(false);
    }

    public async Task<Volunteer?> AddAsync(long id, Volunteer volunteer)
    {
        volunteer.UserId = id;
        await AttachOrganizationAsync(volunteer.OrganizationId).ConfigureAwait(false);
        await _context.Volunteers.AddAsync(volunteer).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);


        var added = await GetByIdAsync(id).ConfigureAwait(false);
        return added;
    }

    public async Task UpdateAsync(Volunteer volunteer)
    {
        if (volunteer.UserId == 0)
        {
            throw new NotFoundException($"Volunteer with id {volunteer.UserId} was not found");
        }
        await AttachOrganizationAsync(volunteer.OrganizationId).ConfigureAwait(false);
        _context.Volunteers.Update(volunteer);

        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task DeleteAsync(long id)
    {
        var foundVolunteer = await _context.Volunteers
                                           .Include(v => v.Establishments)
                                           .FirstOrDefaultAsync(v => v.UserId == id)
                                           .ConfigureAwait(false)
                             ?? throw new NotFoundException($"Volunteer with Id {id} not found for deletion.");

        _context.Volunteers.Remove(foundVolunteer);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    private async Task AttachOrganizationAsync(long organizationId)
    {
        var foundOrganization = await _context.Establishments
                                                          .FirstOrDefaultAsync(e => e.Id == organizationId)
                                                          .ConfigureAwait(false)
                                             ?? throw new NotFoundException($"Volunteer organization with id {organizationId} ws not found");
        _context.Establishments.Attach(foundOrganization);
        _context.Entry(foundOrganization).State = EntityState.Unchanged;
    }
}
