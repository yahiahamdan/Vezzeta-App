using Application.Dtos;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<IdentityResult> Register(RegisterDto registerDto);
        public Task<ApplicationUser> Login(LoginDto loginDto);
    }
}
