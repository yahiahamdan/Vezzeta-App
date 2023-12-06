using Application.Dtos;
using Application.Interfaces.Repositories;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public PatientRepository(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<int> GetCountOfPatients(string roleName)
        {
            var role = await this.roleManager.FindByNameAsync(roleName);

            if (role != null)
            {
                var patientsTotalCount = await this.userManager.GetUsersInRoleAsync(roleName);

                if (patientsTotalCount != null)
                    return patientsTotalCount.Count;
                else
                    return 0;
            }

            return 0;
        }

        public async Task<object> GetPatientById(string patientId)
        {
            var patient = await this.userManager.FindByIdAsync(patientId);

            if (patient == null)
                return null;

            return new
            {
                Image = patient.Image,
                fullName = $"{patient.FirstName} {patient.LastName}",
                email = patient.Email,
                gender = patient.Gender,
                dateOfBirth = patient.DateOfBirth,
            };
        }

        public async Task<List<PatientDto>> GetAllPatient(
            int page,
            int size,
            string searchQuery,
            string roleName
        )
        {
            IList<ApplicationUser> patients = await this.userManager.GetUsersInRoleAsync(roleName);

            var result = patients
                .Where(
                    patient =>
                        patient.Email.Contains(searchQuery)
                        || patient.PhoneNumber.Contains(searchQuery)
                )
                .Skip((page - 1) * size)
                .Take(size)
                .Select(
                    patient =>
                        new PatientDto
                        {
                            patientId = patient.Id,
                            email = patient.Email,
                            fullName = $"{patient.FirstName} {patient.LastName}",
                            image = patient.Image,
                            phoneNumber = patient.PhoneNumber,
                            gender = patient.Gender,
                            dateOfBirth = patient.DateOfBirth
                        }
                )
                .ToList();

            return result;
        }
    }
}
