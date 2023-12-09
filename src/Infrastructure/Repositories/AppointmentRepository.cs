using Application.Dtos;
using Application.Interfaces.Helpers;
using Application.Interfaces.Repositories;
using Core.Enums;
using Core.Models;
using Infrastructure.Database.Context;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IGeneralHelperFunctions generalHelperFunctions;

        public AppointmentRepository(
            ApplicationDbContext context,
            IGeneralHelperFunctions generalHelperFunctions
        )
        {
            this.context = context;
            this.generalHelperFunctions = generalHelperFunctions;
        }

        public string AddTimes(AppointmentDto appointmentDto)
        {
            try
            {
                foreach (var appointment in appointmentDto.Appointments)
                {
                    foreach (var time in appointment.Times)
                    {
                        var existingTime = this.context.Times
                            .Where(currentTime => currentTime.TimeValue == time)
                            .FirstOrDefault();

                        if (existingTime == null)
                        {
                            var addedTime = new Time { TimeValue = time };
                            this.context.Times.Add(addedTime);
                            this.context.SaveChanges();
                        }
                    }
                }

                return "Times added successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<List<int>> GetTimesIds(AppointmentDto appointmentDto)
        {
            List<List<int>> timesIds = new List<List<int>>();

            foreach (var appointment in appointmentDto.Appointments)
            {
                List<int> currentTimesIds = new List<int>();
                foreach (var currentTime in appointment.Times)
                {
                    int timeId = this.context.Times
                        .Where(time => time.TimeValue == currentTime)
                        .Select(time => time.Id)
                        .FirstOrDefault();

                    if (timeId != null)
                        currentTimesIds.Add(timeId);
                }
                timesIds.Add(currentTimesIds);
            }

            return timesIds;
        }

        public List<int> GetDaysIds(AppointmentDto appointmentDto)
        {
            List<int> daysIds = new List<int>();

            foreach (var appointment in appointmentDto.Appointments)
            {
                int dayId = this.context.Days
                    .Where(day => day.Name == Enum.Parse<WeekDaysEnum>(appointment.Day))
                    .Select(day => day.Id)
                    .FirstOrDefault();

                if (dayId != null)
                    daysIds.Add(dayId);
            }

            return daysIds.Distinct().ToList();
        }

        public List<int> AddNewAppointment(AppointmentDto appointmentDto, string doctorId)
        {
            List<int> appointmentIds = new List<int>();
            List<int> daysIds = this.GetDaysIds(appointmentDto);

            for (int i = 0; i < daysIds.Count; i++)
            {
                DateTime dateOfNextDay = this.generalHelperFunctions.GetNextDayOfNextWeek(
                    DateTime.Now,
                    Enum.Parse<WeekDaysEnum>(appointmentDto.Appointments[i].Day)
                );

                var currentAppointment = this.context.Appointments
                    .Where(day => day.DayId == daysIds[i])
                    .Select(appointment => new { appointment.Date, appointment.Id })
                    .FirstOrDefault();

                if (currentAppointment is not null)
                {
                    if (
                        currentAppointment.Date.ToString().Split(' ')[0]
                        != dateOfNextDay.ToString().Split(' ')[0]
                    )
                    {
                        var newAppointment = new Appointment
                        {
                            DoctorId = doctorId,
                            DayId = daysIds[i],
                            Date = dateOfNextDay
                        };

                        this.context.Appointments.Add(newAppointment);
                        this.context.SaveChanges();

                        appointmentIds.Add(newAppointment.Id);
                    }
                }
                else
                {
                    var addedAppointment = new Appointment
                    {
                        DoctorId = doctorId,
                        DayId = daysIds[i],
                        Date = dateOfNextDay
                    };

                    this.context.Appointments.Add(addedAppointment);
                    this.context.SaveChanges();

                    appointmentIds.Add(addedAppointment.Id);
                }
            }

            return appointmentIds.Distinct().ToList();
        }

        public string AddNewAppointmentTime(AppointmentDto appointmentDto, string doctorId)
        {
            try
            {
                this.AddTimes(appointmentDto);
                List<List<int>> timesIds = GetTimesIds(appointmentDto);
                List<int> appointmentsIds = AddNewAppointment(appointmentDto, doctorId);

                for (int i = 0; i < appointmentsIds.Count; i++)
                {
                    for (int j = 0; j < timesIds[i].Count; j++)
                    {
                        this.context.AppointmentTimes.Add(
                            new AppointmentTime
                            {
                                AppointmentId = appointmentsIds[i],
                                TimeId = timesIds[i][j],
                                IsBooked = false
                            }
                        );
                    }
                }

                var examinationPrice = this.context.ExaminationPrices
                    .Where(ex => ex.DoctorId == doctorId)
                    .Select(ex => new { ex.Price, ex.DoctorId })
                    .FirstOrDefault();

                if (examinationPrice is null)
                    this.context.ExaminationPrices.Add(
                        new ExaminationPrice { DoctorId = doctorId, Price = appointmentDto.Price }
                    );

                this.context.SaveChanges();

                return "Succeeded";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
