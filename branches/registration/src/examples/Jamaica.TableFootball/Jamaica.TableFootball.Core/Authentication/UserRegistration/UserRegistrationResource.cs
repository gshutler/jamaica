using System;
using Jamaica.TableFootball.Core.Extensions;

namespace Jamaica.TableFootball.Core.Authentication.UserRegistration
{
    [UriRegistration("/registration")]
    public class UserRegistrationResource
    {
        public string Name { get; set;}
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

        public bool NameError { get; set; }
        public string NameErrorMessage { get; set; }
        
        public bool PasswordError { get; set; }
        public string PasswordErrorMessage { get; set; }

        public void Validate()
        {
            if (!Name.Provided())
            {
                NameError = true;
                NameErrorMessage = "Required";
            }
            if (!Password.Provided())
            {
                PasswordError = true;
                PasswordErrorMessage = "Required";
            }
            else if(Password != PasswordConfirmation)
            {
                PasswordError = true;
                PasswordErrorMessage = "Confirmation did not match";
            }
        }

        public bool Invalid()
        {
            return NameError || PasswordError;
        }
    }
}