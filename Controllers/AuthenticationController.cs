using CommerceClone.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CommerceClone.Controllers
{
    [ApiController]
    [Route("api/auth/[action]/{query?}")]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Providers()
        {
            return Ok(await HttpContext.GetExternalProvidersAsync());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SignIn([FromForm] string provider)
        {
            if (string.IsNullOrWhiteSpace(provider))
                return BadRequest();

            if (!await HttpContext.IsProviderSupportedAsync(provider))
                return BadRequest();

            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, provider);
        }

        [HttpGet]
        [HttpPost]
        public ActionResult SignOutUser()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
