using CommerceClone.Extensions;
using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommerceClone.Controllers
{
    using BCrypt.Net;

    [ApiController]
    [Route("auth/[action]/{query?}")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _auth;
        private readonly IUserRepository _user;

        public AuthenticationController(IAuthenticationRepository auth, IUserRepository user)
        {
            _auth = auth;
            _user = user;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Providers()
        {
            return Ok(await HttpContext.GetExternalProvidersAsync());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> OAuth([FromForm] string provider)
        {
            if (string.IsNullOrWhiteSpace(provider))
                return BadRequest();

            if (!await HttpContext.IsProviderSupportedAsync(provider))
                return BadRequest();

            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, provider);
        }

        [HttpPost]
        public async Task<ActionResult> Email(User user)
        {
            if (ModelState.IsValid)
            {
                var foundUser = _user.GetUserByEmail(user.Email);

                if (foundUser == null)
                    return BadRequest("No user with those credentials found.");

                if (BCrypt.Verify(user.Password, foundUser.Password))
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Email));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            AllowRefresh = true,
                            ExpiresUtc = DateTime.UtcNow.AddDays(1),
                            RedirectUri = "/"
                        }
                    );

                    return Ok();
                }
                return BadRequest();
            }

            return BadRequest();
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
