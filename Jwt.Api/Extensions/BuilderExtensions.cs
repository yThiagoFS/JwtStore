using Jwt.Core;
using Jwt.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using Jwt.Infra.Contexts.AccountContexts.UseCases.Create;
using Jwt.Infra.Contexts.SharedContexts.Services;
using Jwt.Infra.Contexts.SharedContexts.Services.Contracts;
using Jwt.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Jwt.Api.Extensions
{
    public static class BuilderExtensions
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.Database.ConnectionString
                = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            Configuration.Secrets.ApiKey
                = GetSecretsValue(builder, "ApiKey")!;

            Configuration.Secrets.JwtPrivateKey
                = GetSecretsValue(builder, "JwtPrivateKey")!;

            Configuration.Secrets.PasswordSaltKey
                = GetSecretsValue(builder, "PasswordSaltKey")!;

            Configuration.SmtpConfig.Port
                = GetSmtpValues<int>(builder, "Port");

            Configuration.SmtpConfig.ApiKey
                = GetSmtpValues<string>(builder, "ApiKey");

            Configuration.SmtpConfig.Server
                = GetSmtpValues<string>(builder, "Server");

            Configuration.SmtpConfig.SenderEmail
                = GetSmtpValues<string>(builder, "SenderEmail");

            Configuration.RabbitMQConfig.HostName
                = builder.Configuration.GetSection("RabbitMQ").GetValue<string>("HostName")!;
        }

        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            builder
                .Services
                .AddDbContext<AppDbContext>(opts =>
                    opts.UseSqlServer(Configuration.Database.ConnectionString
                        ,optionsBuilder => optionsBuilder.MigrationsAssembly("Jwt.Api")));
        }

        public static void AddJwtAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddAuthorization();
        }

        public static void AddMediatR(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Configuration).Assembly));
        }

        public static void AddMessageServices(this WebApplicationBuilder builder)
            => builder.Services.AddScoped<IMessageService, MessageService>();

        private static string GetSecretsValue(
            WebApplicationBuilder builder,
            string secretName)
                => builder.Configuration.GetSection("Secrets").GetValue<string>(secretName)!;

        private static T GetSmtpValues<T>(WebApplicationBuilder builder, string secretName)
            => builder.Configuration.GetSection("SmtpConfiguration").GetValue<T>(secretName)!;
    }
}
