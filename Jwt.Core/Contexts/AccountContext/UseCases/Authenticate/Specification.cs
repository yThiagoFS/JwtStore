using Flunt.Notifications;
using Flunt.Validations;

namespace Jwt.Core.Contexts.AccountContext.UseCases.Authenticate
{
    public static class Specification 
    {
        public static Contract<Notification> Ensure(Request request)
            => new Contract<Notification>()
                .Requires()
                .IsLowerThan(request.Password.Length, 40, "Password", "Password cannot contain more than 40 characters")
                .IsGreaterOrEqualsThan(request.Password.Length, 8, "Password", "Password must be bigger than 4 characters")
                .IsEmail(request.Email, "Email", "Invalid email.");
    }
}
