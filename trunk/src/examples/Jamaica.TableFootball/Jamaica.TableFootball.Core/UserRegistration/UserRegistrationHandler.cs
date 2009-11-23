using System;
using OpenRasta.Web;

namespace Jamaica.TableFootball.Core.UserRegistration
{
    public class UserRegistrationHandler : Handler
    {
        public OperationResult Get()
        {
            return OK<UserRegistrationResource>();
        }

        public OperationResult Post(UserRegistrationResource userRegistrationResource)
        {
            userRegistrationResource.UsernameError = true;
            userRegistrationResource.UsernameErrorMessage = "Required";

            return BadRequest(userRegistrationResource);
        }
    }
}