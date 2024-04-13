using Core.Bases;
using Core.Enums;

namespace Core.Entities;

public class Motorbike : VehicleBase
{
    public override VehicleType GetVehicleType()
    {
        return VehicleType.Motorbike;
    }
}