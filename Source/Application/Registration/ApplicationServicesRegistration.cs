using Application.Interfaces;
using Application.Services.HolidayCalculationService;
using Application.Services.TollFeeCalculationService;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Registration;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITollFeeCalculationService, TollFeeCalculationService>();
        services.AddScoped<IHolidayCalculationService, HolidayCalculationService>();

        return services;
    }
}