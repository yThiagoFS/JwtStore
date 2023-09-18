using Jwt.Core.Contexts.SharedContext.Entities;
using Jwt.Core.Contexts.SharedContext.ValueObjects;

namespace Jwt.Core.Contexts.AccountContext.Entities
{
    public class User : Entity
    {
        protected User() { }

        public User(string address
            , string? password)
        {
            Email = address;
            Password = new Password(password);
        }

        public User(string name
           , Email emaill
           , Password password)
        {
            Name = name;
            Email = emaill;
            Password = password;
        }

        public string Name { get; private set; } = string.Empty;

        public Email Email { get; private set; } = null!;

        public Password Password { get; private set; } = null!;

        public string Image { get; } = string.Empty;

        public void UpdatePassword(string plainTextPassword, string code)
        {
            if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Restoration code is invalid.");

            Password = new Password(plainTextPassword);
        }

        public void UpdateEmail(Email email) => Email = email;

        public void ChangePassword(string plainTextPassword) => Password = new(plainTextPassword);

  
    }
}
