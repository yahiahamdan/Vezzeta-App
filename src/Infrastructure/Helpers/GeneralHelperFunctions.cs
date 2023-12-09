using Application.Interfaces.Helpers;
using Core.Enums;

namespace Infrastructure.Helpers
{
    public class GeneralHelperFunctions : IGeneralHelperFunctions
    {
        public DateTime GetNextDayOfNextWeek(DateTime currentDate, WeekDaysEnum targetDay)
        {
            int daysUntilTargetDay = ((int)targetDay - (int)currentDate.DayOfWeek + 7) % 7;
            DateTime nextWeekDay = currentDate.AddDays(daysUntilTargetDay + 7);

            return nextWeekDay;
        }
    }
}
