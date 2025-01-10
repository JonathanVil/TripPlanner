using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Infrastructure.Data;
using TripPlanner.Web.Services;

namespace TripPlanner.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton(TimeProvider.System);
        
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();


        return services;
    }
}