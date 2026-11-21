using AutoLog.Models;

namespace AutoLog.Services;

public class VehicleTrackerService
{
    public int GetKmUntilNextService(Vehicle vehicle)
    {
        var nextServiceKm = vehicle.LastServiceMileage + vehicle.ServiceIntervalKm;
        return nextServiceKm - vehicle.CurrentMileage;
    }

    public int GetDaysUntilRegistrationExpiry(Vehicle vehicle)
    {
        return (vehicle.RegistrationExpiry.Date - DateTime.Today).Days;
    }

    public string GetServiceStatus(Vehicle vehicle)
    {
        var kmLeft = GetKmUntilNextService(vehicle);
        if (kmLeft <= 0) return "Prekoračen servis!";
        if (kmLeft <= 1000) return "Uskoro servis";
        return "OK";
    }
}