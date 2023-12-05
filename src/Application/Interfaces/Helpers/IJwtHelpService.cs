using System.Security.Claims;

namespace Application.Interfaces.Helpers
{
    public interface IJwtHelpService
    {
        public string GenerateToken(string email, string userId, string roleName);
        public ClaimsPrincipal DecodeToken(string accessToken);
    }
}
