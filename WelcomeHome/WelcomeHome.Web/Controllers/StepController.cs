using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web.Controllers;

[Route("[controller]")]
[ApiController]
public class StepController : ControllerBase
{
    private readonly IStepService _stepService;

    public StepController(IStepService stepService)
    {
        _stepService = stepService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StepOutDTO>> GetAsync(Guid id)
    {
        var foundStep = await _stepService.GetAsync(id).ConfigureAwait(false);
        return Ok(foundStep);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StepOutDTO>>> GetAllAsync()
    {
        var allSteps = await _stepService.GetAllAsync();
        return Ok(allSteps);
    }

    [HttpGet("/byEstablishmentType/{establishmentTypeId}")]
    public async Task<ActionResult<IEnumerable<StepOutDTO>>> GetByEstablishmentTypeAsync(Guid typeId)
    {
        var stepsByEstablishmentType = await _stepService.GetByEstablishmentTypeIdAsync(typeId);
        return Ok(stepsByEstablishmentType);
    }
    [HttpPost]
    public async Task<IActionResult> AddAsync(StepInDTO newStep)
    {
        await _stepService.AddAsync(newStep).ConfigureAwait(false);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(StepOutDTO updateStep)
    {
        await _stepService.UpdateAsync(updateStep).ConfigureAwait(false);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _stepService.DeleteAsync(id).ConfigureAwait(false);
        return Ok();
    }
}
