using Jwt.Core.Contexts.SharedContext.Exceptions;
using Jwt.Core.Contexts.SharedContext.Extensions;
using System.Text.RegularExpressions;

namespace Jwt.Core.Contexts.SharedContext.ValueObjects
{
    public partial class Email : ValueObject
    {
        private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        protected Email() { }

        public Email(string address)
        {
            IsValidEmail(address);

            Address = address.Trim().ToLower();
        }

        public string Address { get; set; }
        public string Hash => Address.ToBase64();

        public Verification Verification = new();

        public static implicit operator string(Email email) => email.ToString();

        public static implicit operator Email(string address) => new(address);

        public override string ToString() => Address;

        private static void IsValidEmail(string address)
        {
            if (string.IsNullOrEmpty(address)) throw new EmailException("E-mail cannot be null.");

            if (address.Length < 5) throw new EmailException("E-mail cannot contain less than five characters.");

            if (!EmailRegex().IsMatch(address)) throw new EmailException("Invalid e-mail format.");
        }

        public void ResendVerificationCode() => Verification = new();

        [GeneratedRegex(Pattern)]
        private static partial Regex EmailRegex();
    }
}
