using Application.Dtos;
using Application.Interfaces.Helpers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly IMapper mapper;
        private readonly IFileHelperService fileHelperService;
        private readonly IEmailHelperService emailHelperService;

        public DoctorService(
            IDoctorRepository doctorRepository,
            IMapper mapper,
            IFileHelperService fileHelperService,
            IEmailHelperService emailHelperService
        )
        {
            this.mapper = mapper;
            this.doctorRepository = doctorRepository;
            this.fileHelperService = fileHelperService;
            this.emailHelperService = emailHelperService;
        }

        public async Task<int> GetCountOfDoctors(string roleName)
        {
            int totalDoctorsCount = await this.doctorRepository.GetCountOfDoctors(roleName);

            if (totalDoctorsCount == 0)
                return 0;

            return totalDoctorsCount;
        }

        public async Task<object> GetDoctorById(string doctorId)
        {
            var totalDoctorsCount = await this.doctorRepository.GetDoctorById(doctorId);

            if (totalDoctorsCount == null)
                return null;

            return totalDoctorsCount;
        }

        public async Task<IdentityResult> CreateNewDoctor(
            DoctorDto doctorDto,
            string password,
            string specialization
        )
        {
            ApplicationUser user = this.mapper.Map<ApplicationUser>(doctorDto);

            string[] fileInfo = await this.fileHelperService.UploadFile(doctorDto.Image);

            user.Image = fileInfo[0];
            var result = await this.doctorRepository.CreateNewDoctor(
                user,
                password,
                specialization
            );

            if (!result.Succeeded)
            {
                if (File.Exists(Path.Combine(fileInfo[1], fileInfo[0])))
                {
                    string imagePath = Path.Combine(fileInfo[1], fileInfo[0]);
                    fileHelperService.DeleteFile(imagePath);
                }

                await this.doctorRepository.DeleteSingleDoctor(user);
            }

            string credentials =
                $"Your Credentials:\nEmail: {doctorDto.Email}\nPassword: {doctorDto.Password}";
            this.emailHelperService.SendEmail(
                doctorDto.Email,
                "Doctor registered successfully.",
                credentials
            );

            return result;
        }

        public async Task<IdentityResult> UpdateDoctorById(
            UpdateDoctorDto doctorDto,
            string doctorId,
            string specialization
        )
        {
            ApplicationUser user = this.mapper.Map<ApplicationUser>(doctorDto);

            string[] fileInfo = await this.fileHelperService.UploadFile(doctorDto.Image);

            var result = await this.doctorRepository.UpdateDoctorById(
                user,
                specialization,
                doctorId,
                fileInfo[0]
            );

            if (!result.Item1.Succeeded)
            {
                if (File.Exists(Path.Combine(fileInfo[1], fileInfo[0])))
                {
                    string imagePath = Path.Combine(fileInfo[1], fileInfo[0]);
                    fileHelperService.DeleteFile(imagePath);
                }
            }
            else
            {
                if (File.Exists(Path.Combine(fileInfo[1], result.Item2)))
                {
                    string imagePath = Path.Combine(fileInfo[1], result.Item2);
                    fileHelperService.DeleteFile(imagePath);
                }
            }

            return result.Item1;
        }

        public async Task<List<ReturnDoctorDto>> GetAllDoctors(
            int page,
            int limit,
            string searchQuery
        )
        {
            List<ReturnDoctorDto> doctors = await this.doctorRepository.GetAllDoctors(
                page,
                limit,
                searchQuery
            );

            return doctors;
        }
    }
}
