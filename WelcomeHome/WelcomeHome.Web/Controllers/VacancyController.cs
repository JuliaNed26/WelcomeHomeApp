using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO.VacancyDTO;
using WelcomeHome.Services.Services.VacancyService;

namespace WelcomeHome.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacancyController : ControllerBase
{
    private readonly IVacancyService _vacancyService;

    public VacancyController(IVacancyService vacancyService)
    {
        _vacancyService = vacancyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VacancyDTO>>> GetAllAsync()
    {
        var allVacancies = await _vacancyService.GetAllAsync().ConfigureAwait(false);
        return Ok(allVacancies);
    }
}
