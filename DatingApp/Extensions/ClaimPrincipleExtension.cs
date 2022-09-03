using System.Security.Claims;

namespace DatingApp.Extensions
{
    public static class ClaimPrincipleExtension
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
        var us= user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return us;
        }
    }
}
