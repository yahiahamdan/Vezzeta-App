using Application.Dtos;
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

            string specialization = String.Empty;

            if (doctor != null)
                specialization = this.context.Specializations
                    .Where(spec => spec.Id == doctor.SpecializationId)
                    .Select(spec => spec.Title)
                    .SingleOrDefault();

            return new
            {
                email = doctor.Email,
                firstName = doctor.FirstName,
                lastName = doctor.LastName,
                image = doctor.Image,
                phoneNumber = doctor.PhoneNumber,
                gender = doctor.Gender,
                dateOfBirth = doctor.DateOfBirth,
                Specialization = specialization,
            };
        }

        public async Task<ApplicationUser> FindDoctorById(string doctorId)
        {
            return await this.userManager.FindByIdAsync(doctorId);
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

        public async Task DeleteSingleDoctor(ApplicationUser user)
        {
            await this.userManager.DeleteAsync(user);
        }

        public async Task<(IdentityResult, string)> UpdateDoctorById(
            ApplicationUser doctor,
            string specialization,
            string doctorId,
            string imageName
        )
        {
            int specializationId = await this.context.Specializations
                .Where(spec => spec.Title == specialization)
                .Select(spec => spec.Id)
                .FirstOrDefaultAsync();

            ApplicationUser user = await this.userManager.FindByIdAsync(doctorId);

            string oldImage = user.Image;

            user.Email = doctor.Email;
            user.UserName = doctor.Email;
            user.FirstName = doctor.FirstName;
            user.LastName = doctor.LastName;
            user.PhoneNumber = doctor.PhoneNumber;
            user.Gender = doctor.Gender;
            user.DateOfBirth = doctor.DateOfBirth;
            user.Image = imageName;

            var result = await this.userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                user.SpecializationId = specializationId;
                this.context.SaveChanges();
            }

            return (result, oldImage);
        }

        public async Task<List<ReturnDoctorDto>> GetAllDoctors(
            int page,
            int limit,
            string searchQuery
        )
        {
            IList<ApplicationUser> doctors = await this.userManager.GetUsersInRoleAsync(
                RolesEnum.Doctor.ToString()
            );

            var result = doctors
                .Join(
                    this.context.Specializations,
                    doctor => doctor.SpecializationId,
                    specialization => specialization.Id,
                    (doctor, specialization) =>
                        new { Doctor = doctor, Specialization = specialization }
                )
                .Where(
                    joined =>
                        joined.Doctor.Email.Contains(searchQuery)
                        || joined.Doctor.PhoneNumber.Contains(searchQuery)
                )
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(
                    joined =>
                        new ReturnDoctorDto
                        {
                            doctorId = joined.Doctor.Id,
                            email = joined.Doctor.Email,
                            fullName = $"{joined.Doctor.FirstName} {joined.Doctor.LastName}",
                            image = joined.Doctor.Image,
                            phoneNumber = joined.Doctor.PhoneNumber,
                            gender = joined.Doctor.Gender,
                            dateOfBirth = joined.Doctor.DateOfBirth,
                            specialization = joined.Specialization.Title
                        }
                )
                .ToList();

            return result;
        }

        public List<int> GetAppointmentsByDoctorId(string doctorId)
        {
            List<int> appointmentIds = new List<int>();

            var appointmentsIds = this.context.Appointments
                .Where(appointment => appointment.DoctorId == doctorId)
                .Select(appointment => appointment.Id);

            foreach (var id in appointmentsIds)
                appointmentIds.Add(id);

            return appointmentIds;
        }

        public List<int> GetAppointmentTimeByAppointmentId(List<int> appointmentIds)
        {
            List<int> appointmentTimeIds = new List<int>();
            foreach (var id in appointmentIds)
            {
                int appointmentTimeId = this.context.AppointmentTimes
                    .Where(appointmentTime => appointmentTime.AppointmentId == id)
                    .Select(appointmentTime => appointmentTime.Id)
                    .FirstOrDefault();

                appointmentTimeIds.Add(appointmentTimeId);
            }

            return appointmentTimeIds;
        }

        public bool CheckUserDeletionEligibility(List<int> appointmentTimeIds)
        {
            foreach (var id in appointmentTimeIds)
            {
                var booking = this.context.Bookings
                    .Where(booking => booking.AppointmentTimeId == id)
                    .FirstOrDefault();

                if (booking != null)
                    return true;
            }

            return false;
        }
    }
}
