using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WelcomeHome.DAL.Models;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly UserManager<User> _userManager;
	private readonly IMapper _mapper;

	public UserController(UserManager<User> userManager, IMapper mapper)
	{
		_userManager = userManager;
		_mapper = mapper;
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<UserOutDTO>> GetAsync(Guid id)
	{
		var foundUser = await _userManager.FindByIdAsync(id.ToString()).ConfigureAwait(false);
		return Ok(_mapper.Map<UserOutDTO>(foundUser));
	}

	[HttpGet]
	public ActionResult<IEnumerable<UserOutDTO>> GetAll()
	{
		var allUsers = _userManager.Users.ToList();
		var result = _mapper.Map<List<UserOutDTO>>(allUsers);
		return Ok(result);
	}

}
