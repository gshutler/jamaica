using System;
using System.Collections.Generic;
using Jamaica.Handlers;
using Jamaica.Helpers;
using Jamaica.Repositories;
using Jamaica.Security;
using NHibernate;
using NHibernate.Criterion;
using OpenRasta;
using OpenRasta.Configuration;
using OpenRasta.Web;
using OR.Throwaway.Resources;

namespace OR.Throwaway.Handlers
{
    public class LoginCreationHandler : Handler
    {
        public static void Registration()
        {
            ResourceSpace.Has.ResourcesOfType<LoginCreationResource>()
                .AtUri("/login/create")
                .HandledBy<LoginCreationHandler>()
                .RenderedByAspx("~/Views/LoginCreation.aspx");
        }

        private readonly IUserRepository userRepository;
        private readonly IResponse response;

        public LoginCreationHandler(IUserRepository userRepository, IResponse response)
        {
            this.userRepository = userRepository;
            this.response = response;
        }

        public OperationResult Get()
        {
            return OK(new LoginCreationResource());
        }

        public OperationResult Post(LoginCreationResource login)
        {
            var existingUser = userRepository.GetByUsername(login.Username);

            if (existingUser != null)
            {
                return BadRequest(login, UsernameTakenErrors());
            }

            var newUser = new User {Username = login.Username};

            newUser.SetPasswordHash(login.Password);

            userRepository.Add(newUser);

            response.SetAuthenticationCookies(newUser);

            return SeeOther(CreateUri.For<HomeResource>());
        }

        private static List<Error> UsernameTakenErrors()
        {
            return new List<Error>
                       {
                           new Error {Message = "Username already in use"}
                       };
        }
    }
}