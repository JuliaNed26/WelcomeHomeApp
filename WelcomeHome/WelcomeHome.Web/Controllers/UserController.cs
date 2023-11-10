using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private IUserService _userService;

	public UserController(IUserService userService)
	{
		_userService = userService;
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<UserOutDTO>> GetAsync(Guid id)
	{
		var foundUser = await _userService.GetAsync(id).ConfigureAwait(false);
		return Ok(foundUser);
	}

	[HttpGet]
	public ActionResult<IEnumerable<UserOutDTO>> GetAll()
	{
		var allUsers = _userService.GetAll();
		return Ok(allUsers);
	}

	[HttpPost]
	public async Task<IActionResult> AddAsync(UserInDTO newUser)
	{
		await _userService.AddAsync(newUser).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPut]
	public async Task<IActionResult> UpdateAsync(UserOutDTO updateUser)
	{
		await _userService.UpdateAsync(updateUser).ConfigureAwait(false);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteAsync(Guid id)
	{
		await _userService.DeleteAsync(id).ConfigureAwait(false);
		return NoContent();
	}
}
