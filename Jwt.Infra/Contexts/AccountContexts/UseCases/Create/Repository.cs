using Jwt.Core.Contexts.AccountContext.Entities;
using Jwt.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using Jwt.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Jwt.Infra.Contexts.AccountContexts.UseCases.Create
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context) => _context = context;

        public async Task<bool> AnyAsync(string email, CancellationToken cancellationToken)
             => await _context.Users.AsNoTracking().AnyAsync(x => x.Email.Address == email, cancellationToken);

        public async Task SaveAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
