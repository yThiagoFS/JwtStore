using Jwt.Core.Contexts.AccountContext.Entities;
using Jwt.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;

namespace Jwt.Core.Contexts.AccountContext.UseCases.Authenticate
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IRepository _repository;

        public Handler(IRepository repository) => _repository = repository;

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            #region Validar a requisição

            try
            {
                var result = Specification.Ensure(request);

                if (!result.IsValid)
                    return new Response("Invalid request.", 400, result.Notifications);
            }
            catch
            {
                return new Response("It wasn't possible to validate your requisition.", 500, null);
            }

            #endregion

            #region Validar o perfil

            User? user;

            try
            {
                user = await _repository.GetByUserEmailAsync(request.Email, cancellationToken);

                if (user is null)
                    return new Response("Profile not found.", 404);
            }
            catch
            {
                return new Response("It wasn't possible to search for the user.", 500);
            }

            #endregion

            #region Checar senha
            if (!user.Password.Challenge(request.Password))
                return new Response("Invalid user or password.", 400);
            #endregion

            #region Checar verificação do e-mail

            try
            {
                if (!user.Email.Verification.IsActive)
                    return new Response("Account inactive.", 400);
            }
            catch
            {
                return new Response("It wasn't possible to verify your profile.", 500);
            }

            #endregion

            #region Retornar os dados

            try
            {
                var data = new ResponseData
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Email = user.Email,
                    Roles = Array.Empty<string>()
                };

                return new Response(string.Empty, data);
            }
            catch
            {
                return new Response("Something occurred while processing your request.", 500);
            }
            #endregion
        }
    }
}
