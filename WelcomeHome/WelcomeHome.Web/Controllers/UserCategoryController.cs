using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services.UserCategoryService;

namespace WelcomeHome.Web.Controllers;

[Route("[controller]")]
[ApiController]
public class UserCategoryController : ControllerBase
{
    private readonly IUserCategoryService _userCategoryService;

    public UserCategoryController(IUserCategoryService userCategoryService)
    {
        _userCategoryService = userCategoryService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserCategoryOutDTO>> GetAsync(int id)
    {
        var foundCategory = await _userCategoryService.GetAsync(id).ConfigureAwait(false);
        return Ok(foundCategory);
    }

    [HttpGet]
    public ActionResult<IEnumerable<UserCategoryOutDTO>> GetAll()
    {
        var allCategories = _userCategoryService.GetAll();
        return Ok(allCategories);
    }
}
