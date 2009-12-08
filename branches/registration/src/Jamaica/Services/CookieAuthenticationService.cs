using System;
using Jamaica.Repositories;
using Jamaica.Security;
using OpenRasta.Web;

namespace Jamaica.Services
{
    public interface ICookieAuthenticationService
    {
        /// <summary>
        /// Retrieves the security principal authorized by the request's cookies.
        /// </summary>
        /// <returns>
        /// Authorized security principal, if one exists a static "anonymous" instance.
        /// Should never return null.
        /// </returns>
        ISecurityPrincipal AuthorizedSecurityPrincipal();
        void AuthorizeSecurityPrincipal(ISecurityPrincipal securityPrincipal);
        void ClearCookies();
    }

    public class CookieAuthenticationService : ICookieAuthenticationService
    {
        const string AuthName = "__authName";
        const string AuthHash = "__authHash";

        readonly IRequest request;
        readonly ISecurityPrincipalRepository securityPrincipalRepository;
        readonly IResponse response;

        public CookieAuthenticationService(IRequest request, ISecurityPrincipalRepository securityPrincipalRepository, IResponse response)
        {
            this.request = request;
            this.response = response;
            this.securityPrincipalRepository = securityPrincipalRepository;
        }

        public ISecurityPrincipal AuthorizedSecurityPrincipal()
        {
            if (BothAuthenticationCookiesArePresent())
            {
                return SecurityPrincipalMatchingAuthenticationCookies() ?? User.Anonymous;
            }
            return User.Anonymous;
        }

        public void ClearCookies()
        {
            RemoveCookieIfSupplied(AuthName);
            RemoveCookieIfSupplied(AuthHash);
        }

        public void AuthorizeSecurityPrincipal(ISecurityPrincipal securityPrincipal)
        {
            response.Cookies.Add(new ResponseCookie(AuthName, securityPrincipal.Name) {Path = "/"});
            response.Cookies.Add(new ResponseCookie(AuthHash, securityPrincipal.Hash) {Path = "/"});
        }

        bool BothAuthenticationCookiesArePresent()
        {
            return request.Cookies.Exists(AuthName) && request.Cookies.Exists(AuthHash);
        }

        ISecurityPrincipal SecurityPrincipalMatchingAuthenticationCookies()
        {
            return securityPrincipalRepository.GetByNameAndHash(request.Cookies.Get(AuthName), request.Cookies.Get(AuthHash));
        }

        void RemoveCookieIfSupplied(string cookieName)
        {
            if (request.Cookies.Exists(cookieName))
                response.Cookies.Remove(cookieName);
        }
    }
}