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

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var socialPayouts = _socialPayoutService.GetAll();
            return Ok(socialPayouts);

        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var socialPayout = await _socialPayoutService.GetAsync(id);
            return Ok(socialPayout);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _socialPayoutService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync (SocialPayoutInDTO updatePayOut)
        {
            await _socialPayoutService.UpdateAsync(updatePayOut).ConfigureAwait(false);
            return Ok();
        }
    }
}
