using MediatR;

namespace Jwt.Api.Extensions
{
    public static class AccountContextExtensions
    {

        public static void AddAccountContext(this WebApplicationBuilder builder)
        {
            #region Create

            builder.Services.AddTransient<
                Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository
                ,Infra.Contexts.AccountContexts.UseCases.Create.Repository>();
            builder.Services.AddScoped<
                Core.Contexts.AccountContext.UseCases.Create.Contracts.IService
                ,Infra.Contexts.AccountContexts.UseCases.Create.Service>();

            #endregion

            #region Authenticate

            builder.Services.AddTransient<
                Core.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository
                , Infra.Contexts.AccountContexts.UseCases.Authenticate.Repository>();

            #endregion
        }

        public static void MapAccountEndpoints(this WebApplication app)
        {
            #region Create

            app.MapPost("api/v1/users", async (
                Core.Contexts.AccountContext.UseCases.Create.Request request
                , IRequestHandler<
                    Core.Contexts.AccountContext.UseCases.Create.Request
                    , Core.Contexts.AccountContext.UseCases.Create.Response> handler) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Created($"api/v1/users/{result.Data?.Id}", result)
                    : Results.Json(result, statusCode: result.Status);
            });

            #endregion

            #region Authenticate

            app.MapPost("api/v1/authenticate", async (
                Core.Contexts.AccountContext.UseCases.Authenticate.Request request
                , IRequestHandler<
                    Core.Contexts.AccountContext.UseCases.Authenticate.Request
                    , Core.Contexts.AccountContext.UseCases.Authenticate.Response> handler) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.Status);
            });

            #endregion
        }
    }
}
