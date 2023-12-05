using Application.Interfaces.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Helpers
{
    public class JwtHelperService : IJwtHelpService
    {
        private SymmetricSecurityKey key;
        private IConfiguration configuration;

        public JwtHelperService(IConfiguration configuration)
        {
            this.configuration = configuration;
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
        }

        public ClaimsPrincipal DecodeToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler().ValidateToken(
                accessToken,
                new TokenValidationParameters()
                {
                    IssuerSigningKey = key,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                },
                out SecurityToken securityToken
            );

            return handler;
        }

        public string GenerateToken(string email, string userId, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim("userId", userId),
                new Claim("role", role),
            };

            var signingCredentials = new SigningCredentials(
                this.key,
                SecurityAlgorithms.HmacSha256Signature
            );

            var description = new SecurityTokenDescriptor()
            {
                Issuer = configuration["JWT:Issuer"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(30),
                Audience = configuration["JWT:Audience"],
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims),
            };

            var handler = new JwtSecurityTokenHandler();
            var securedToken = handler.CreateToken(description);

            return handler.WriteToken(securedToken);
        }
    }
}
