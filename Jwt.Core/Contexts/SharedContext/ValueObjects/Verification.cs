using Jwt.Core.Contexts.SharedContext.Exceptions;

namespace Jwt.Core.Contexts.SharedContext.ValueObjects
{
    public class Verification : ValueObject
    {
        public Verification() { }

        public string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();

        public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);

        public DateTime? VerifiedAt { get; private set; } = null;

        public bool IsActive => VerifiedAt != null && ExpiresAt == null;

        public void Verify(string code)
        {
            if (IsActive) throw new VerificationException("The code is already verified.");

            if (ExpiresAt < DateTime.UtcNow) throw new VerificationException("This code has already expired");

            if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))throw new VerificationException("The code informed is not correct.");

            ExpiresAt = null;
            VerifiedAt = DateTime.UtcNow;
        }
    }
}
