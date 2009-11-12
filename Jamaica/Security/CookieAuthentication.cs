using System;
using Jamaica.Pipeline.Contributors;
using OpenRasta.Configuration;
using OpenRasta.Configuration.Fluent;
using OpenRasta.Web;

namespace Jamaica.Security
{
    public static class CookieAuthentication
    {
        public const string AuthHash = "__authhash";
        public const string AuthName = "__authname";

        public static void JamaicanCookieAuthentication(this IUses anchor)
        {
            anchor.PipelineContributor<CookieAuthenticationContributor>();
        }

        public static void SetAuthenticationCookies(this IResponse response, User user)
        {
            response.Cookies.Add(new ResponseCookie(AuthName, user.Username) {Path = "/"});
            response.Cookies.Add(new ResponseCookie(AuthHash, user.PasswordHash) { Path = "/" });
        }

        public static void RemoveAuthenticationCookies(this IResponse response)
        {
            response.Cookies.Remove(AuthName);
            response.Cookies.Remove(AuthHash);
        }
    }
}