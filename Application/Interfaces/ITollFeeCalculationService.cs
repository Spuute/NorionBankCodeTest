using Core.Entities;

namespace Application.Interfaces;

public interface ITollFeeCalculationService
{
    int CalculateTotalTollFeeForDay(VehicleBase vehicle, DateTime[] dateTimesPassedToll);
}