using Jwt.Core.Contexts.AccountContext.Entities;
using Jwt.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using Jwt.Core.Contexts.SharedContext.ValueObjects;
using MediatR;

namespace Jwt.Core.Contexts.AccountContext.UseCases.Create
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IRepository _repository;
        private readonly IService _service;

        public Handler(IRepository repository, IService service)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(IRepository));
            _service = service ?? throw new ArgumentNullException(nameof(IService)); ;
        }

        public async Task<Response> Handle(
            Request request
            ,CancellationToken cancellationToken)
        {
            #region Validar request

            try
            {
                var validator = Specification.Ensure(request);

                if (!validator.IsValid)
                    return new Response("Invalid request.", 400, validator.Notifications);
            }
            catch
            {
                //TODO: criar exception especifica
                return new Response("We couldn't validate your request.Please, try again later.", 500);
            }

            #endregion

            #region Gerar objetos

            Email email;
            Password password;
            User user;

            try
            {
                email = new Email(request.Email);
                password = new Password(request.Password);
                user = new User(request.Name, email, password);
            }
            catch(Exception ex)
            {
                return new Response($"{ex.Message}", 400);
            }

            #endregion

            #region Verificar se o usuário existe no banco

            try
            {
                var exists = await _repository.AnyAsync(request.Email, cancellationToken);
                if (exists)
                    return new Response("This is already in use.", 400);
            }
            catch
            {
                return new Response("Failed request to verify the email.", 400);
            }

            #endregion

            #region Persistir dados

            try
            {
                await _repository.SaveAsync(user, cancellationToken);
            }
            catch
            {
                return new Response("Failed to create the user.", 500);
            }

            #endregion

            #region E-mail de ativação

            try
            {
                await _service.SendVerificationEmailAsync(user, cancellationToken);
            }
            catch
            {
                //TODO: Criar ação compensatória
            }

            return new Response("Account created", new ResponseData(user.Id, user.Name, user.Email));

            #endregion

        }

    }
}
