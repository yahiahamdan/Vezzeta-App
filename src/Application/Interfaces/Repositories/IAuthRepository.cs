using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        public Task<IdentityResult> Register(ApplicationUser user, string password);
    }
}
