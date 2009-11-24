using System;
using System.Collections.Generic;
using System.Linq;

namespace Jamaica.Security
{
    public class User : ISecurityPrincipal
    {
        public static readonly User Anonymous = new User("Anonymous");

        public virtual string Name { get; protected set; }
        public virtual string Salt { get; protected set; }
        public virtual string Hash { get; protected set; }
        public virtual IList<Role> Roles { get; set; }

        IEnumerable<ISecurityRole> ISecurityPrincipal.Roles
        {
            get { return Roles.Cast<ISecurityRole>(); }
        }

        protected User()
        {
            Roles = new List<Role>();
        }

        public User(string username) : this()
        {
            Name = username;
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

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            /* not a user */
            if (!typeof(User).IsAssignableFrom(obj.GetType())) return false;
            /* are the same object */
            if (ReferenceEquals(obj, this)) return true;
            /* have the same name */
            return Name == ((User) obj).Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}