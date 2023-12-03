using Core.Enums;
using Core.Models;

namespace Infrastructure.Database.Data
{
    internal static class DaysSeeding
    {
        public static List<Day> SeedWeekDays()
        {
            List<Day> weekDays = [];

            int idCounter = 0;

            foreach (WeekDaysEnum day in Enum.GetValues(typeof(WeekDaysEnum)))
                weekDays.Add(new Day() { Id = ++idCounter, Name = day });

            return weekDays;
        }
    }
}
