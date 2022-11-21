
using System.Security.Claims;

namespace DatingApp.Extensions
{
    public static class ClaimPrincipleExtension
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
        var us= user.FindFirst(ClaimTypes.Name)?.Value;
            return us; 
        }
        public static int GetUserId(this ClaimsPrincipal user)
        {
           
            var test= user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           // int test= int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return int.Parse(test);
            
        }
    }
}
