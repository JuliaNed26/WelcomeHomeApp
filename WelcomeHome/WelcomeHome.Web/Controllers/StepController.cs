using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class StepController : ControllerBase
{
    private readonly IStepService _stepService;

    public StepController(IStepService stepService)
    {
        _stepService = stepService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StepOutDTO>> GetAsync(int id)
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
    public async Task<ActionResult<IEnumerable<StepOutDTO>>> GetByEstablishmentTypeAsync(int typeId)
    {
        var stepsByEstablishmentType = await _stepService.GetByEstablishmentTypeIdAsync(typeId);
        return Ok(stepsByEstablishmentType);
    }
    [HttpPost]
    [Authorize(Policy = nameof(AuthorizationPolicies.VolunteerOnly))]
    public async Task<IActionResult> AddAsync(StepInDTO newStep)
    {
        await _stepService.AddAsync(newStep).ConfigureAwait(false);
        return Ok();
    }

    [HttpPut]
    [Authorize(Policy = nameof(AuthorizationPolicies.VolunteerOnly))]
    public async Task<IActionResult> UpdateAsync(StepOutDTO updateStep)
    {
        await _stepService.UpdateAsync(updateStep).ConfigureAwait(false);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = nameof(AuthorizationPolicies.VolunteerOnly))]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _stepService.DeleteAsync(id).ConfigureAwait(false);
        return Ok();
    }
}
