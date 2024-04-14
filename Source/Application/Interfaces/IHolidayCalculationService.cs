namespace Application.Interfaces;

public interface IHolidayCalculationService
{
    bool IsWeekend(DateTime date);
    bool IsJuly(DateTime date);
    bool IsTollFreeDate(DateTime date);
}