using Application.Interfaces.Repositories;
using Core.Enums;
using Core.Models;
using Infrastructure.Database.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public DoctorRepository(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context
        )
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<int> GetCountOfDoctors(string roleName)
        {
            IList<ApplicationUser> doctors = await this.userManager.GetUsersInRoleAsync(roleName);

            return doctors.Count;
        }

        public async Task<object> GetDoctorById(string doctorId)
        {
            ApplicationUser doctor = await this.userManager.FindByIdAsync(doctorId);

            return new
            {
                email = doctor.Email,
                firstName = doctor.FirstName,
                lastName = doctor.LastName,
                image = doctor.Image,
                phoneNumber = doctor.PhoneNumber,
                gender = doctor.Gender,
                dateOfBirth = doctor.DateOfBirth,
                Specialization = doctor.Specialization,
            };
        }

        public async Task<IdentityResult> CreateNewDoctor(
            ApplicationUser user,
            string password,
            string specialization
        )
        {
            int specializationId = await this.context.Specializations
                .Where(spec => spec.Title == specialization)
                .Select(spec => spec.Id)
                .FirstOrDefaultAsync();

            user.UserName = user.Email;

            var result = await this.userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                user.SpecializationId = specializationId;
                this.context.SaveChanges();
            }

            try
            {
                await this.userManager.AddToRoleAsync(user, RolesEnum.Doctor.ToString());

                return result;
            }
            catch (Exception ex)
            {
                if (result.Succeeded)
                    await this.userManager.DeleteAsync(user);

                return result;
            }
        }

        public async Task<IdentityResult> DeleteSingleDoctor(ApplicationUser user)
        {
            return await this.userManager.DeleteAsync(user);
        }
    }
}
