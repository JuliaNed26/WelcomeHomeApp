using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web.Controllers;

[Route("[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventOutDTO>> GetAsync(Guid id)
    {
        var foundEvent = await _eventService.GetAsync(id).ConfigureAwait(false);
        return Ok(foundEvent);
    }

    [HttpGet]
    public ActionResult<IEnumerable<EventOutDTO>> GetAll()
    {
        var allEvents = _eventService.GetAll();
        return Ok(allEvents);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(EventInDTO newEvent)
    {
        await _eventService.AddAsync(newEvent).ConfigureAwait(false);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(EventOutDTO updateEvent)
    {
        await _eventService.UpdateAsync(updateEvent).ConfigureAwait(false);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _eventService.DeleteAsync(id).ConfigureAwait(false);
        return NoContent();
    }
}
