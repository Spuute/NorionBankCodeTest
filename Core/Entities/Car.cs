using Core.Enums;

namespace Core.Entities;

public class Car : VehicleBase
{
    public override VehicleType GetVehicleType()
    {
        return VehicleType.Car;
    }
}