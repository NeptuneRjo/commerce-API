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

    public class OAuthBody
    {
        public string Provider { get; set; }
        public string Redirect { get; set; }
    }

    public class EmailBody
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [ApiController]
    [Route("v1/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAdminRepository _admin;

        public AuthenticationController(IAdminRepository admin)
        {
            _admin = admin;
        }

        [HttpGet]
        public async Task<ActionResult> Providers()
        {
            var providers = new List<Dictionary<string, string>>();

            foreach (var provider in await HttpContext.GetExternalProvidersAsync())
            {
                providers.Add(new Dictionary<string, string>()
            {
                { "Name", provider.Name },
                { "DisplayName", provider.DisplayName }
            });
            }

            return Ok(providers);
        }

        [HttpPost("oauth")]
        public async Task<ActionResult> OAuth(OAuthBody body)
        {
            if (string.IsNullOrWhiteSpace(body.Provider))
                return BadRequest("No provider specified");

            if (string.IsNullOrWhiteSpace(body.Redirect))
                return BadRequest("No redirect url specified");

            if (!await HttpContext.IsProviderSupportedAsync(body.Provider))
                return BadRequest("Provider not supported");

            return Challenge(new AuthenticationProperties { RedirectUri = body.Redirect }, body.Provider);
        }

        [HttpPost("email")]
        public async Task<ActionResult> Email(EmailBody body)
        {
            if (string.IsNullOrWhiteSpace(body.Email))
                return BadRequest("No email provided");

            if (string.IsNullOrWhiteSpace(body.Password))
                return BadRequest("No password provided");

            Admin admin = _admin.GetByEmail(body.Email);

            if (admin == null)
                return BadRequest("No admin with those credentials found");

            if (BCrypt.Verify(body.Password, admin.Password))
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, body.Email));
                identity.AddClaim(new Claim(ClaimTypes.Name, body.Email));

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
            return BadRequest("Failed to authenticate admin");
        }

        [HttpGet("signout")]
        [HttpPost("signout")]
        public ActionResult SignOutUser()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
