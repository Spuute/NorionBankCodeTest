using Application.Interfaces;
using Application.Services.Dates;
using PublicHoliday;

namespace Application.Services.HolidayCalculationService;

public class HolidayCalculationService : IHolidayCalculationService
{
    public bool IsWeekend(DateTime date)
    {
        return HolidayDateHelper.IsWeekend(date);
    }
    
    public bool IsTollFreeDate(DateTime date)
    {
        var tollFreeDates = new List<DateTime>();
        var publicHolidays = new SwedenPublicHoliday().PublicHolidays(date.Year);

        foreach (var day in publicHolidays)
        {
            tollFreeDates.Add(day);

            var previousDay = HolidayDateHelper.GetPreviousWorkDay(day);
            if (HolidayDateHelper.IsChargeableDayBeforeHolidays(previousDay))
                tollFreeDates.Add(HolidayDateHelper.GetPreviousWorkDay(day));
        }

        return tollFreeDates.Contains(date);
    }
}