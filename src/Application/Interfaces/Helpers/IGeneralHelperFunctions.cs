using Core.Enums;

namespace Application.Interfaces.Helpers
{
    public interface IGeneralHelperFunctions
    {
        public DateTime GetNextDayOfNextWeek(DateTime currentDate, WeekDaysEnum targetDay);
    }
}
