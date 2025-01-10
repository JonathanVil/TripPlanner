using Blazored.LocalStorage;
using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Infrastructure.Data;
using TripPlanner.Infrastructure.Identity;
using TripPlanner.Web.Services;

namespace TripPlanner.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton(TimeProvider.System);
        
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddTransient<IIdentityService, IdentityService>();
        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddBlazoredLocalStorage();

        return services;
    }
}