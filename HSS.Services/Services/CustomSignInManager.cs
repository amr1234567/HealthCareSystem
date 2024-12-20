using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using HSS.Domain.BaseModels;
using HSS.Domain.Models;
using Microsoft.Extensions.Options;
using HSS.Domain.Helpers;

namespace HSS.Services.Services
{
    public class CustomSignInManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CookieConfiguration _cookieConfiguration;

        public CustomSignInManager(IHttpContextAccessor httpContextAccessor, IOptions<CookieConfiguration> options)
        {
            _httpContextAccessor = httpContextAccessor;
            _cookieConfiguration = options.Value;
        }

        public async Task<bool> SignInAsync(IdentityUser user, bool isPersistent, List<Claim>? claims = null)
        {
            if (user == null) return false;

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddHours(_cookieConfiguration.ExpireTime),
                AllowRefresh = true
            };

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return true;
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }

}
