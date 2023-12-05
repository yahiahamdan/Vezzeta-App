using Application.Dtos;
using Application.Interfaces.Helpers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly IMapper mapper;
        private readonly IFileHelperService fileHelperService;

        public AuthService(
            IAuthRepository authRepository,
            IMapper mapper,
            IFileHelperService fileHelperService
        )
        {
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
    }
}
