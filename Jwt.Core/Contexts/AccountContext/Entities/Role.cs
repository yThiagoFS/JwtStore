using Jwt.Core.Contexts.SharedContext.Entities;

namespace Jwt.Core.Contexts.AccountContext.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; } = string.Empty;

        public List<User> Users { get; set; } = new();
    }
}
