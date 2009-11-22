using System;
using System.Collections.Generic;
using System.Linq;

namespace Jamaica.Security
{
    public class User
    {
        public static readonly User Anonymous = new User {Username = "Anonymous"};

        public virtual int Id { get; protected set; }
        public virtual string Username { get; protected set; }
        public virtual string Salt { get; protected set; }
        public virtual string Hash { get; protected set; }
        public virtual IList<Role> Roles { get; set; }

        protected User()
        {
            Roles = new List<Role>();
        }

        public User(string username) : this()
        {
            Username = username;
        }

        public virtual void AddRole(Role role)
        {
            Roles.Add(role);
        }

        public virtual void SetPassword(string password)
        {
            Salt = Cryptography.GenerateHexSaltString();
            Hash = (password + Salt).GenerateHexHashString();
        }

        public virtual bool VerifyPassword(string suppliedPassword)
        {
            return Hash == (suppliedPassword + Salt).GenerateHexHashString();
        }

        public virtual string[] GetRoleNames()
        {
            return Roles.Select(role => role.Name).ToArray();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            /* not a user */
            if (!typeof(User).IsAssignableFrom(obj.GetType())) return false;
            /* are the same object */
            if (ReferenceEquals(obj, this)) return true;
            /* this object isn't saved */
            if (Id == 0) return false;
            /* have the same id */
            return Id == ((User) obj).Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}