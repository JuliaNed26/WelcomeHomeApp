using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentOutDTO>> GetAsync(int id)
    {
        var foundDocument = await _documentService.GetAsync(id).ConfigureAwait(false);
        return Ok(foundDocument);
    }

    [HttpGet]
    public ActionResult<IEnumerable<DocumentOutDTO>> GetAll()
    {
        var allDocuments = _documentService.GetAll();
        return Ok(allDocuments);
    }

    [HttpGet("step/{stepId}/needed")]
    public ActionResult<IEnumerable<DocumentOutDTO>> GetByStepNeeded(int stepId)
    {
        var allDocumentsNeededForStep = _documentService.GetByStepNeeded(stepId);
        return Ok(allDocumentsNeededForStep);
    }

    [HttpGet("step/{stepId}/received")]
    public ActionResult<IEnumerable<DocumentOutDTO>> GetByStepReceived(int stepId)
    {
        var allDocumentsReceivedForStep = _documentService.GetByStepReceived(stepId);
        return Ok(allDocumentsReceivedForStep);
    }

    [HttpPost]
    [Authorize(Policy = nameof(AuthorizationPolicies.VolunteerOnly))]
    public async Task<IActionResult> AddAsync(DocumentInDTO newDocument)
    {
        await _documentService.AddAsync(newDocument).ConfigureAwait(false);
        return NoContent();
    }

    [HttpPut]
    [Authorize(Policy = nameof(AuthorizationPolicies.VolunteerOrModerator))]
    public async Task<IActionResult> UpdateAsync(DocumentOutDTO updateDocument)
    {
        await _documentService.UpdateAsync(updateDocument).ConfigureAwait(false);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = nameof(AuthorizationPolicies.VolunteerOrModerator))]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _documentService.DeleteAsync(id).ConfigureAwait(false);
        return NoContent();
    }
}
