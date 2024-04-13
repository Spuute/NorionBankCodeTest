using Application.Interfaces;
using Core.DomainObjects;
using Core.Entities;
using Core.Enums;

namespace Application.Services.TollFeeCalculationService;

public class TollFeeCalculationService(IHolidayCalculationService holidayCalculationService)
{
    public int CalculateTotalTollFeeForDay(VehicleBase vehicleBase, DateTime[] dates)
    {
        var intervalStart = dates[0];
        var totalFee = 0;
        foreach (var date in dates)
        {
            var nextFee = GetTollFeeForSinglePassage(date, vehicleBase);
            var tempFee = GetTollFeeForSinglePassage(intervalStart, vehicleBase);

            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            var minutes = diffInMillies/1000/60;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }

        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    private int GetTollFeeForSinglePassage(DateTime date, VehicleBase vehicle)
    {
        if (IsTollFreeVehicle(vehicle))
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