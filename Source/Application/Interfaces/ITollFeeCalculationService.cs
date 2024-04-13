using Core.Bases;

namespace Application.Interfaces;

public interface ITollFeeCalculationService
{
    int CalculateTotalTollFeeForDay(VehicleBase vehicle, DateTime[] dateTimesPassedToll);
}