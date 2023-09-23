using CommerceApi.BLL.Utilities.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CommerceApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthenticationController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetProviders()
        {
            try
            {
                return Ok(await HttpContext.GetExternalProvidersAsync());
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost]
        public async Task<IActionResult> OAuth([FromForm] string provider, string redirectURL)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(redirectURL) || string.IsNullOrWhiteSpace(provider) || !await HttpContext.IsProviderSupportedAsync(provider))
                    return BadRequest();

                string redirectURI = "api/authentication/callback/" + redirectURL;

                return Challenge(new AuthenticationProperties { RedirectUri = redirectURI }, provider);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet, HttpPost, Route("callback/{redirectURL}")]
        public async Task<IActionResult> Callback(string redirectURL)
        {
            try
            {
                string token = CreateToken();

                string returnURL = (!redirectURL.EndsWith("/"))
                    ? redirectURL += "/login?token=" + token
                    : redirectURL += "login?token=" + token;

                return Redirect(returnURL);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet, HttpPost, Route("signout")]
        public async Task<IActionResult> SignOutUser()
        {
            try
            {
                return SignOut(new AuthenticationProperties { }, CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        private string CreateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("username", username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
