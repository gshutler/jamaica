using System;
using Jamaica.TableFootball.Core.UserRegistration;
using NUnit.Framework;
using OpenRasta.Web;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.UserRegistration
{
    public class SubmittingRegistrationDetailsWithoutAUsername : Specification
    {
        UserRegistrationResource userRegistrationResource;
        OperationResult result;

        protected override void Given()
        {
            userRegistrationResource = new UserRegistrationResource
                                           {
                                               Username = "",
                                               Password = "password",
                                               PasswordConfirmation = "password"
                                           };
        }

        protected override void When()
        {
            result = Subject<UserRegistrationHandler>().Post(userRegistrationResource);
        }

        [Then]
        public void BadRequest()
        {
            Verify(result, Is.TypeOf<OperationResult.BadRequest>());
        }

        [Then]
        public void UserRegistrationResourceIsSentBack()
        {
            Verify(result.ResponseResource, Is.SameAs(userRegistrationResource));
        }

        [Then]
        public void UsernameErrorAttached()
        {
            Verify(userRegistrationResource.UsernameError, Is.True);
        }

        [Then]
        public void UsernameErrorMessageAttached()
        {
            Verify(userRegistrationResource.UsernameErrorMessage, Is.EqualTo("Required"));
        }
    }
}