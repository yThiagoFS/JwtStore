using Jwt.Api.Extensions;
using Jwt.Core.Contexts.AccountContext.UseCases.Create;
using Jwt.Core.Contexts.AccountContext.UseCases.Create.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();
builder.AddAccountContext();
builder.AddMediatR();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapAccountEndpoints();

app.Run();


