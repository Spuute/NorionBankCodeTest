namespace Application.Helpers.Dates;

public static class HolidayDateHelper
{
    public static bool IsWeekend(DateTime date)
    {
        return date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    }

    public static DateTime GetPreviousWorkDay(DateTime currentDay)
    {
        return currentDay.AddDays(-1);
    }
    
    public static DateTime CalculateMidsummer(int year)
    {
        var dateTime = new DateTime(year, 6, 19);
        for (var i = 0; i < 7; i++)
        {
            if (dateTime.AddDays(i).DayOfWeek == DayOfWeek.Friday)
                return dateTime.AddDays(i);
        }

        // TODO: Create custom exception.
        throw new ArgumentOutOfRangeException("Something went wrong when calculating midsummer.");
    }
    
    public static bool IsChargeableDayBeforeHolidays(DateTime currentDay)
    {
        var dayBeforeMidsummer = GetPreviousWorkDay(CalculateMidsummer(currentDay.Year));
        var dayBeforeChristmas = new DateTime(currentDay.Year, 12, 23);
        var dayBeforeNewYears = new DateTime(currentDay.Year, 12, 30);
        
        return currentDay.Date == dayBeforeMidsummer || 
               currentDay.Date == dayBeforeChristmas ||
               currentDay.Date == dayBeforeNewYears;
    }
}