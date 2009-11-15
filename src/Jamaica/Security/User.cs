using System;
using System.Collections.Generic;

namespace Jamaica.Security
{
    public class User
    {
        public static readonly User Anonymous = new User {Username = "Anonymous"};

        public string Username { get; protected set; }
        public IList<Role> Roles { get; protected set; }

        protected User()
        {
            Roles = new List<Role>();
        }

        public User(string username) : this()
        {
            Username = username;
        }

        public void AddRole(Role role)
        {
            Roles.Add(role);
        }
    }
}