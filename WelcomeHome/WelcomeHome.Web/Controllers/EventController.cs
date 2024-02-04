using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO.EventDto;
using WelcomeHome.Services.Services.EventService;

namespace WelcomeHome.Web.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<EventOutDTO>> GetAll()
    {
        var allEvents = _eventService.GetAll();
        return Ok(allEvents);
    }

    [HttpGet("psychologicalServices")]
    public async Task<ActionResult<IEnumerable<EventOutDTO>>> GetPsychologicalServicesAsync()
    {
        var psychologicalServices = await _eventService.GetPsychologicalServicesAsync().ConfigureAwait(false);
        return Ok(psychologicalServices);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventOutDTO>> GetAsync(int id)
    {
        var foundEvent = await _eventService.GetAsync(id).ConfigureAwait(false);
        return Ok(foundEvent);
    }

    [HttpPost]
    [Authorize(Policy = nameof(AuthorizationPolicies.VolunteerOnly))]
    public async Task<IActionResult> AddAsync(EventInDTO newEvent)
    {
        await _eventService.AddAsync(newEvent).ConfigureAwait(false);
        return NoContent();
    }

    [HttpPost("psychologicalService")]
    [Authorize(Policy = nameof(AuthorizationPolicies.VolunteerOnly))]
    public async Task<IActionResult> AddPsychologicalServiceAsync(EventInDTO newEvent)
    {
        await _eventService.AddPsychologicalServiceAsync(newEvent).ConfigureAwait(false);
        return NoContent();
    }

    [HttpPut]
    [Authorize(Policy = nameof(AuthorizationPolicies.VolunteerOrModerator))]
    public async Task<IActionResult> UpdateAsync(EventOutDTO updateEvent)
    {
        await _eventService.UpdateAsync(updateEvent).ConfigureAwait(false);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = nameof(AuthorizationPolicies.VolunteerOrModerator))]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _eventService.DeleteAsync(id).ConfigureAwait(false);
        return NoContent();
    }
}
