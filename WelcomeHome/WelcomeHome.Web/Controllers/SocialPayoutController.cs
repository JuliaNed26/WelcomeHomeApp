using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialPayoutController : ControllerBase
    {
        private readonly ISocialPayoutService _socialPayoutService;
        public SocialPayoutController(ISocialPayoutService socialPayoutService)
        {
            _socialPayoutService = socialPayoutService;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(SocialPayoutInDTO newPayOut)
        {
            await _socialPayoutService.AddAsync(newPayOut);
            return NoContent();
        }
    }
}
