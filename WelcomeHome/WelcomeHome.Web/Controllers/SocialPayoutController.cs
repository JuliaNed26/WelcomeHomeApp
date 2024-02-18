using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SocialPayoutController : ControllerBase
    {
        private readonly ISocialPayoutService _socialPayoutService;
        public SocialPayoutController(ISocialPayoutService socialPayoutService)
        {
            _socialPayoutService = socialPayoutService;
        }
        [HttpPost]
        [Authorize(Policy = nameof(AuthorizationPolicies.VolunteerOnly))]
        public async Task<IActionResult> AddAsync(SocialPayoutInDTO newPayOut)
        {
            await _socialPayoutService.AddAsync(newPayOut);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<SocialPayoutListItemDTO> GetAll()
        {
            var socialPayouts = _socialPayoutService.GetAll();
            return Ok(socialPayouts);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SocialPayoutOutDTO>> GetById(int id)
        {
            var socialPayout = await _socialPayoutService.GetAsync(id);
            return Ok(socialPayout);

        }

        [HttpDelete]
        [Authorize(Policy = nameof(AuthorizationPolicies.VolunteerOrModerator))]
        public async Task<IActionResult> Delete(int id)
        {
            await _socialPayoutService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = nameof(AuthorizationPolicies.VolunteerOrModerator))]
        public async Task<IActionResult> Update(SocialPayoutOutDTO newSocialPayout)
        {
            await _socialPayoutService.UpdateAsync(newSocialPayout);
            return Ok();
        }

    }
}