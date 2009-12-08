using System;
using Jamaica.TableFootball.Core.Authentication.UserRegistration;
using NUnit.Framework;
using OpenRasta.Web;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.Authentication.UserRegistration
{
    public class RegistrationDetailsAreMissingAName : Specification
    {
        UserRegistrationResource userRegistrationResource;
        OperationResult result;

        protected override void Given()
        {
            userRegistrationResource = new UserRegistrationResource
                                           {
                                               Name = "",
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
        public void NameErrorAttached()
        {
            Verify(userRegistrationResource.NameError, Is.True);
        }

        [Then]
        public void NameErrorMessageAttached()
        {
            Verify(userRegistrationResource.NameErrorMessage, Is.EqualTo("Required"));
        }
    }
}