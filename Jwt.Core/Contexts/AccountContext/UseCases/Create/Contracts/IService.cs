using Jwt.Core.Contexts.AccountContext.Entities;

namespace Jwt.Core.Contexts.AccountContext.UseCases.Create.Contracts
{
    public interface IService
    {
        Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken);
    }
}
