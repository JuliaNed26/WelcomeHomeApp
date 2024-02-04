using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;
using WelcomeHome.Services.Services.EstablishmentService;

namespace WelcomeHome.Web.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class VolunteerController : ControllerBase
{
    private readonly IVolunteerService _volunteerService;

    public VolunteerController(IVolunteerService volunteerService)
    {
        _volunteerService = volunteerService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VolunteerOutDTO>> GetAsync(int id)
    {
        var foundVolunteer = await _volunteerService.GetAsync(id).ConfigureAwait(false);
        return Ok(foundVolunteer);
    }

    [HttpGet]
    public ActionResult<IEnumerable<VolunteerOutDTO>> GetAll()
    {
        var allVolunteers = _volunteerService.GetAll();
        return Ok(allVolunteers);
    }

    [HttpPost("organization")]
    [Authorize(Policy = nameof(AuthorizationPolicies.VolunteerOnly))]
    public async Task<IActionResult> AddVolunteerOrganizationAsync(EstablishmentVolunteerInDTO newEstablishment)
    {
        await _volunteerService.AddVolunteerOrganizationAsync(newEstablishment).ConfigureAwait(false);
        return NoContent();
    }

    //TODO: Moderator can update volunteer info or delete it

    //[HttpPut]
    //public async Task<IActionResult> UpdateAsync(VolunteerOutDTO updateVolunteer)
    //{
    //	await _volunteerService.UpdateAsync(updateVolunteer).ConfigureAwait(false);
    //	return NoContent();
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteAsync(Guid id)
    //{
    //	await _volunteerService.DeleteAsync(id).ConfigureAwait(false);
    //	return NoContent();
    //}
}
