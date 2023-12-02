using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class EstablishmentController : ControllerBase
{
    private readonly IEstablishmentService _establishmentService;

    public EstablishmentController(IEstablishmentService establishmentService)
    {
        _establishmentService = establishmentService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstablishmentOutDTO>> GetAsync(int id)
    {
        var foundEstablishment = await _establishmentService.GetAsync(id).ConfigureAwait(false);
        return Ok(foundEstablishment);
    }

    [HttpGet]
    public ActionResult<IEnumerable<EstablishmentOutDTO>> GetAll()
    {
        var allEstablishments = _establishmentService.GetAll();
        return Ok(allEstablishments);
    }

    [HttpGet("byType/{cityId}")]
    public ActionResult<IEnumerable<EstablishmentOutDTO>> GetByEstablishmentType(int typeId)
    {
        var establishmentsByType = _establishmentService.GetByEstablishmentType(typeId);
        return Ok(establishmentsByType);
    }

    [HttpGet("byCity/{cityId}")]
    public ActionResult<IEnumerable<EstablishmentOutDTO>> GetByCity(int cityId)
    {
        var establishmentsByCity = _establishmentService.GetByCity(cityId);
        return Ok(establishmentsByCity);
    }

    [HttpGet("/byType/{typeId}/byCity/{cityId}")]
    public ActionResult<IEnumerable<EstablishmentOutDTO>> GetByTypeAndCity(int typeId, int cityId)
    {
        var establishmentsByTypeAndCity = _establishmentService.GetByTypeAndCity(typeId, cityId);
        return Ok(establishmentsByTypeAndCity);
    }

    [HttpGet("types")]
    public ActionResult<IEnumerable<EstablishmentTypeOutDTO>> GetAllEstablishmentTypes()
    {
        var establishmentTypes = _establishmentService.GetAllEstablishmentTypes();
        return Ok(establishmentTypes);
    }

    [HttpPost]
    [Authorize(Roles = "volunteer")]
    public async Task<IActionResult> AddAsync(EstablishmentInDTO newEstablishment)
    {
        await _establishmentService.AddAsync(newEstablishment).ConfigureAwait(false);
        return NoContent();
    }

    [HttpPut]
    [Authorize(Roles = "volunteer")]
    public async Task<IActionResult> UpdateAsync(EstablishmentOutDTO updateEstablishment)
    {
        await _establishmentService.UpdateAsync(updateEstablishment).ConfigureAwait(false);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "volunteer")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _establishmentService.DeleteAsync(id).ConfigureAwait(false);
        return NoContent();
    }
}
