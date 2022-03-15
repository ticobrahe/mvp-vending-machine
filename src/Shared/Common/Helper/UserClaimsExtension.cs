using System;
using System.Linq;
using System.Security.Claims;

namespace Common.Helper
{
    public static class UserClaimsExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            return Guid.Parse(userId);
        }
    }
}
