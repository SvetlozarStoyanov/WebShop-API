using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebShop.Extensions
{

    [ApiController]
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
