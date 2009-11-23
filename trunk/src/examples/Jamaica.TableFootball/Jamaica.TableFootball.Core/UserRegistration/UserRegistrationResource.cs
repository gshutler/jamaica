using System;

namespace Jamaica.TableFootball.Core.UserRegistration
{
    public class UserRegistrationResource
    {
        public string Username { get; set;}
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

        public bool UsernameError { get; set; }
        public string UsernameErrorMessage { get; set; }
    }
}