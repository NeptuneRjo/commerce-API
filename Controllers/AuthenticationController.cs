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
    [Route("v1/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAdminRepository _admin;
        private readonly IUserRepository _user;

        public AuthenticationController(IAdminRepository admin, IUserRepository user)
        {
            _admin = admin;
            _user = user;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Providers()
        {
            return Ok(await HttpContext.GetExternalProvidersAsync());
        }

        [HttpPost("/oauth")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> OAuth([FromForm] string provider)
        {
            if (string.IsNullOrWhiteSpace(provider))
                return BadRequest();

            if (!await HttpContext.IsProviderSupportedAsync(provider))
                return BadRequest();

            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, provider);
        }

        [HttpPost("/email/user")]
        public async Task<ActionResult> UserEmail(User user)
        {
            if (ModelState.IsValid)
            {
                var foundUser = _user.GetByEmail(user.Email);

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

        [HttpPost("/email/admin")]
        public async Task<ActionResult> AdminEmail(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var foundAdmin = _admin.GetByEmail(admin.Email);

                if (foundAdmin == null)
                    return BadRequest("No admin with those credentials found.");

                if (BCrypt.Verify(admin.Password, foundAdmin.Password))
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, admin.Email));
                    identity.AddClaim(new Claim(ClaimTypes.Name, admin.Email));

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

        [HttpGet("/signout")]
        [HttpPost("/signout")]
        public ActionResult SignOutUser()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
