using Jwt.Core.Contexts.AccountContext.Entities;
using Jwt.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using Jwt.Core.Contexts.SharedContext.ValueObjects;
using Jwt.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Jwt.Infra.Contexts.AccountContexts.UseCases.Authenticate
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context) => _context = context;

        public async Task<User?> GetByUserEmailAsync(string email, CancellationToken cancellationToken)
            => await _context
                     .Users
                     .AsNoTracking()
                     .FirstOrDefaultAsync(u => u.Email.Address == email, cancellationToken: cancellationToken);
    }
}
