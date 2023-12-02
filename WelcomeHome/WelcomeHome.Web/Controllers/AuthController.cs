using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IVolunteerService _volunteerService;

        public AuthController(IAuthService authService, IVolunteerService volunteerService)
        {
            _authService = authService;
            _volunteerService = volunteerService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO user)
        {
            var token = await _authService.LoginUserAsync(user);
            if (token != null)
            {
                return Ok(token);
            }

            return BadRequest();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO user)
        {
            var registered = await _authService.RegisterUserAsync(user);

            if (registered != null)
            {
                return Ok("User registered successfully!");
            }

            return BadRequest();
        }

        [HttpPost("RegisterVolunteer")]
        public async Task<IActionResult> RegisterVolunteer(VolunteerRegisterDTO volunteer)
        {
            var registered = await _volunteerService.RegisterVolunteerAsync(volunteer);

            if (registered != null)
            {
                return Ok("Volunteer registered successfully!");
            }

            return BadRequest();
        }

    }
}