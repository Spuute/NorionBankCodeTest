using Application.Interfaces;
using Core.Entities;

namespace UI;

public class App(ITollFeeCalculationService tollFeeCalculationService)
{
    public void Run()
    {
        var vehicle = new Car();
        var passageTimes = new DateTime[]
        {
            new(2024, 04, 12, 06, 05, 00), 
            new(2024, 04, 12, 06, 59, 00) 
        };

        var tollFee = tollFeeCalculationService.CalculateTotalTollFeeForDay(vehicle, passageTimes);
        Console.WriteLine(tollFee);
    }
}