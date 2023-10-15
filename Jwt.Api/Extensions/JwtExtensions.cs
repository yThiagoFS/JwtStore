using Jwt.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jwt.Api.Extensions
{
    public static class JwtExtensions
    {
        public static string Generate(Jwt.Core.Contexts.AccountContext.UseCases.Authenticate.ResponseData data)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(data),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = credentials
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(Jwt.Core.Contexts.AccountContext.UseCases.Authenticate.ResponseData user)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim("Id", user.Id));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            claimsIdentity.AddClaims(user.Roles.Select(us => new Claim(ClaimTypes.Role, us)));

            return claimsIdentity;
        }
    }
}
