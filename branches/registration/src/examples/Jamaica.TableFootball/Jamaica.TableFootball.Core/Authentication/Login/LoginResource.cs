using System;

namespace Jamaica.TableFootball.Core.Authentication.Login
{
    public class LoginResource
    {
        public LoginResource()
        {
            Name = "";
            Password = "";
        }

        public string Name { get; set; }
        public string Password { get; set; }

        public bool LoginFailed { get; private set; }
        public string ErrorMessage { get; private set; }

        public void SetErrorMessage(string errorMessage)
        {
            LoginFailed = true;
            ErrorMessage = errorMessage;
        }
    }
}