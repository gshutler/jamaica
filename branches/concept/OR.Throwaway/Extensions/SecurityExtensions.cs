using System.Security.Principal;

namespace OR.Throwaway.Extensions
{
    public static class SecurityExtensions
    {
        public static bool LoggedIn(this IPrincipal principal)
        {
            return principal.Identity.IsAuthenticated;
        }
    }
}