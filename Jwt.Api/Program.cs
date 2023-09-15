using Jwt.Api.Extensions;
using Jwt.Core.Contexts.AccountContext.UseCases.Create;
using Jwt.Core.Contexts.AccountContext.UseCases.Create.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();
builder.RegistrateServices();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/", () => "Hello World!");

app.MapGet("/test", async () =>
{
    using var scope = app.Services.CreateScope();
    var repositoryDependency = scope.ServiceProvider.GetRequiredService<IRepository>();
    var serviceDependency = scope.ServiceProvider.GetRequiredService<IService>();

    await new Handler(repositoryDependency,serviceDependency)
            .Handle(new Request("Thiago", "thi.ferreira.silva03@gmail.com", "123456"), new CancellationToken());
});

app.Run();


