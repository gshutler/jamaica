using System;
using System.Security.Principal;

namespace Jamaica.Security
{
    public class User 
    {
        public static readonly User Anonymous = new User {Id = -1, Username = "anon"};

        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string PasswordSalt { get; protected set; }
        public virtual string PasswordHash { get; protected set; }

        public virtual void SetPasswordHash(string password)
        {
            PasswordSalt = Guid.NewGuid().ToString();
            PasswordHash = GeneratePasswordHash(password);
        }

        public virtual bool CorrectPassword(string password)
        {
            return PasswordHash == GeneratePasswordHash(password);
        }

        public virtual bool CorrectCookieHash(string cookieHash)
        {
            return PasswordHash == cookieHash;
        }

        private string GeneratePasswordHash(string password)
        {
            return (Username + password + PasswordSalt).ToHash();
        }

        public virtual IPrincipal CreatePrincipal()
        {
            var identity = new GenericIdentity(Username);
            return new GenericPrincipal(identity, new string[] {});
        }
    }
}