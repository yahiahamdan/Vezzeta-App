using Application.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<IdentityResult> Register(RegisterDto registerDto);
    }
}
