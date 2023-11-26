using Microsoft.AspNetCore.Mvc;
using System.Web;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web.Controllers
{
    [Route("[controller]")]
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
        public async Task<ActionResult<string>> Login(UserLoginDTO user)
        {
            var loginResponse = await _authService.LoginUserAsync(user).ConfigureAwait(false);
            AddRefreshTokenToCookie(loginResponse.RefreshToken);
            return Ok(loginResponse.JwtToken);
		}

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO user)
        {
            var registered = await _authService.RegisterUserAsync(user).ConfigureAwait(false);

            if (registered != null)
            {
                return Ok("User registered successfully!");
            }

            return BadRequest();
        }

        [HttpPost("RegisterVolunteer")]
        public async Task<IActionResult> RegisterVolunteer(VolunteerRegisterDTO volunteer)
        {
            var registered = await _volunteerService.RegisterVolunteerAsync(volunteer).ConfigureAwait(false);

            if (registered != null)
            {
                return Ok("Volunteer registered successfully!");
            }

            return BadRequest();
        }

        [HttpPut("Refresh/{refreshToken}")]
        public async Task<ActionResult<string>> RefreshJwtTokenAsync(string refreshToken)
        {
            var refreshTokenInRightFormat = HttpUtility.UrlDecode(refreshToken);
			var refreshedTokens = await _authService.RefreshTokenAsync(refreshTokenInRightFormat)
                                                    .ConfigureAwait(false);
			AddRefreshTokenToCookie(refreshedTokens.RefreshToken);
			return Ok(refreshedTokens.JwtToken);
		}

		private void AddRefreshTokenToCookie(string refreshToken)
		{
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true
			};
			Response.Cookies.Append("x-refresh-token", refreshToken, cookieOptions);
		}
	}
}