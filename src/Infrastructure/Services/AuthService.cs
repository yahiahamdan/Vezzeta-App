using Application.Dtos;
using Application.Interfaces.Helpers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly IMapper mapper;
        private readonly IFileHelperService fileHelperService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IJwtHelpService jwtHelpService;

        public AuthService(
            IAuthRepository authRepository,
            IMapper mapper,
            IFileHelperService fileHelperService,
            IHttpContextAccessor httpContextAccessor,
            IJwtHelpService jwtHelpService
        )
        {
            this.jwtHelpService = jwtHelpService;
            this.httpContextAccessor = httpContextAccessor;
            this.authRepository = authRepository;
            this.mapper = mapper;
            this.fileHelperService = fileHelperService;
        }

        public async Task<IdentityResult> Register(RegisterDto registerDto)
        {
            string filePath = null;
            string fileName = null;

            ApplicationUser user = this.mapper.Map<ApplicationUser>(registerDto);

            if (registerDto.Image is not null)
            {
                string[] fileInfo = await this.fileHelperService.UploadFile(registerDto.Image);
                user.Image = fileInfo[0];
                fileName = fileInfo[0];
                filePath = fileInfo[1];
            }

            var result = await this.authRepository.Register(user, registerDto.Password);

            if (!result.Succeeded)
            {
                if (
                    filePath != null
                    && fileName != null
                    && File.Exists(Path.Combine(filePath, fileName))
                )
                {
                    string imagePath = Path.Combine(filePath, fileName);
                    fileHelperService.DeleteFile(imagePath);
                }
            }

            return result;
        }

        public async Task<ApplicationUser> Login(LoginDto loginDto)
        {
            var (result, roles) = await this.authRepository.Login(
                loginDto.Email,
                loginDto.Password
            );

            if (result != null)
            {
                var roleName = roles.FirstOrDefault();

                var accessToken = jwtHelpService.GenerateToken(result.Email, result.Id, roleName);

                httpContextAccessor.HttpContext.Response.Cookies.Append(
                    "accessToken",
                    accessToken,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddDays(30),
                    }
                );
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
