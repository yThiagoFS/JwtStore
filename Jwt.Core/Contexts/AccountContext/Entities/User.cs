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

        public string Name { get; private set; } = string.Empty;

        public Email Email { get; private set; } = null!;

        public Password Password { get; private set; } = null!;

        public string Image { get; } = string.Empty;

        public void UpdatePassword(string plainTextPassword, string code)
        {
            if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Restoration code is invalid");

            var password = new Password(plainTextPassword);
            Password = password;
        }

        public void UpdateEmail(Email email) => Email = email;

        public void ChangePassword(string plainTextPassword) => Password = new(plainTextPassword);
    }
}
