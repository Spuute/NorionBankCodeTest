using Application.Interfaces;
using Core.Bases;
using Core.DomainObjects;
using Core.Enums;

namespace Application.Services.TollFeeCalculationService;

public class TollFeeCalculationService(IHolidayCalculationService holidayCalculationService) : ITollFeeCalculationService
{
    public int CalculateTotalTollFeeForDay(VehicleBase vehicle, DateTime[] dates)
    {
        const int dailyMaxFee = 60;
        var firstPassage = dates[0];
        var totalFee = 0;
        foreach (var currentPassage in dates)
        {
            var currentPassageFee = GetTollFeeForSinglePassage(currentPassage, vehicle);
            var firstPassageFee = GetTollFeeForSinglePassage(firstPassage, vehicle);
            
            var minutes = GetTimeDifferenceInMinutes(firstPassage, currentPassage);

            if (minutes <= 60)
            {
                totalFee = UpdateTotalFeeForPassageWithinTheHour(firstPassageFee, currentPassageFee);
            }
            else
            {
                totalFee += currentPassageFee;
            }
        }

        return CheckIfFeeExceedsMaxFeeLimit(totalFee) ? dailyMaxFee : totalFee;
    }
    
    private static double GetTimeDifferenceInMinutes(DateTime firstPassage, DateTime currentPassage)
    {
        var timeDiffInMinutes = currentPassage - firstPassage;
        return timeDiffInMinutes.TotalMinutes;
    }
    
    private static bool CheckIfFeeExceedsMaxFeeLimit(int currentFee)
    {
        return currentFee > 60;
    }
    
    private static int UpdateTotalFeeForPassageWithinTheHour(int firstPassageFee, int currentPassageFee)
    {
        return currentPassageFee > firstPassageFee ? currentPassageFee : firstPassageFee;
    }

    private int GetTollFeeForSinglePassage(DateTime date, VehicleBase vehicle)
    {
        if (IsTollFreeVehicle(vehicle))
            return 0;
        
        if (holidayCalculationService.IsJuly(date))
            return 0;

        if (holidayCalculationService.IsWeekend(date))
            return 0;
        
        if (holidayCalculationService.IsTollFreeDate(date))
            return 0;
        
        return _tollFeeTable.FirstOrDefault(x => x.TimeIsWithinBounds(date.TimeOfDay))?.Price ?? 0;
    }
    
    private static bool IsTollFreeVehicle(VehicleBase vehicleBase)
    {
        return Enum.TryParse<TollFreeVehicles>(vehicleBase.GetVehicleType().ToString(), out _);
    }
    
    private readonly List<TollFeeMap> _tollFeeTable =
    [
        new TollFeeMap
        {
            TimeFrom = new TimeSpan(6, 0, 0),
            TimeTo = new TimeSpan(6, 29, 59),
            Price = 8
        },

        new TollFeeMap
        {
            TimeFrom = new TimeSpan(6, 30, 0),
            TimeTo = new TimeSpan(6, 59, 59),
            Price = 13
        },

        new TollFeeMap
        {
            TimeFrom = new TimeSpan(7, 0, 0),
            TimeTo = new TimeSpan(7, 59, 59),
            Price = 18
        },

        new TollFeeMap
        {
            TimeFrom = new TimeSpan(8, 0, 0),
            TimeTo = new TimeSpan(8, 29, 59),
            Price = 13
        },

        new TollFeeMap
        {
            TimeFrom = new TimeSpan(8, 30, 0),
            TimeTo = new TimeSpan(14, 59, 59),
            Price = 8
        },

        new TollFeeMap
        {
            TimeFrom = new TimeSpan(15, 0, 0),
            TimeTo = new TimeSpan(15, 29, 59),
            Price = 13
        },

        new TollFeeMap
        {
            TimeFrom = new TimeSpan(15, 30, 0),
            TimeTo = new TimeSpan(16, 59, 59),
            Price = 18
        },

        new TollFeeMap
        {
            TimeFrom = new TimeSpan(17, 0, 0),
            TimeTo = new TimeSpan(17, 59, 59),
            Price = 13
        },

        new TollFeeMap
        {
            TimeFrom = new TimeSpan(18, 0, 0),
            TimeTo = new TimeSpan(18, 29, 59),
            Price = 8
        }
    ];
}