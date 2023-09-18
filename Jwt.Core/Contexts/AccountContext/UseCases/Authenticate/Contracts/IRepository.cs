using Jwt.Core.Contexts.AccountContext.Entities;
using Jwt.Core.Contexts.SharedContext.ValueObjects;

namespace Jwt.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts
{
    public interface IRepository
    {
        Task<User?> GetByUserEmailAsync(string email, CancellationToken cancellationToken);
    }
}
