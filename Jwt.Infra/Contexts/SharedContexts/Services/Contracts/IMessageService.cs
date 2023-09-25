using Jwt.Core.Contexts.SharedContext.ValueObjects;

namespace Jwt.Infra.Contexts.SharedContexts.Services.Contracts
{
    public interface IMessageService
    {
        Task SendMessageAsync<T>(T value);
    }
}
