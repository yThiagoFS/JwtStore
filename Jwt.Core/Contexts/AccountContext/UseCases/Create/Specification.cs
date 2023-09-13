using Flunt.Notifications;
using Flunt.Validations;

namespace Jwt.Core.Contexts.AccountContext.UseCases.Create
{
    public static class Specification
    {
        public static Contract<Notification> Ensure(Request request)
            => new Contract<Notification>()
                .Requires()
                .IsLowerThan(request.Name.Length, 160, "Name", "The name cannot contains more than 160 characters")
                .IsGreaterThan(request.Name.Length, 3, "Name", "The name must have more than 3 characters")
                .IsLowerThan(request.Password, 40, "Password", "The password cannot contains more than 40 characters")
                .IsGreaterThan(request.Password, 8, "Password", "The password must have more than 4 characters")
                .IsEmail(request.Email, "Password", "Invalid e-mail");
    }
}
