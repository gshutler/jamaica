using System;
using Jamaica.Repositories;
using Jamaica.Security;
using OpenRasta.Web;

namespace Jamaica.Services
{
    public interface ICookieAuthenticationService
    {
        User AuthorizedUser();
        void ClearCookies();
        // todo : add a way to set authentication cookies
        // this will mean that by implementing this interface
        // you can change how authentication is persisted 
        // between the server and client via cookies
        // void SetAuthorizedUser(User user);
    }

    public class CookieAuthenticationService : ICookieAuthenticationService
    {
        const string AuthName = "__authName";
        const string AuthHash = "__authHash";

        readonly IRequest request;
        readonly IUserRepository userRepository;
        readonly IResponse response;

        public CookieAuthenticationService(IRequest request, IUserRepository userRepository, IResponse response)
        {
            this.request = request;
            this.response = response;
            this.userRepository = userRepository;
        }

        public User AuthorizedUser()
        {
            if (request.Cookies.Exists(AuthName) && request.Cookies.Exists(AuthHash))
            {
                return userRepository.GetByUsernameAndHash(
                           request.Cookies.Get(AuthName), request.Cookies.Get(AuthHash))
                    ?? User.Anonymous;
            }

            return User.Anonymous;
        }

        public void ClearCookies()
        {
            RemoveCookieIfSupplied(AuthName);
            RemoveCookieIfSupplied(AuthHash);
        }

        void RemoveCookieIfSupplied(string cookieName)
        {
            if (request.Cookies.Exists(cookieName))
                response.Cookies.Remove(cookieName);
        }
    }
}