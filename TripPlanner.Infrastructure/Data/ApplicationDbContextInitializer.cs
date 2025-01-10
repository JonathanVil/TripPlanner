using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TripPlanner.Infrastructure.Data;

public static class InitializerExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app, bool useMigrations = true)
    {
        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initializer.InitializeAsync(useMigrations);

        await initializer.SeedAsync();
    }
}

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitializeAsync(bool useMigrations = true)
    {
        try
        {
            if (useMigrations)
            {
                await _context.Database.MigrateAsync();
            }
            else
            {
                await _context.Database.EnsureCreatedAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
    }
}
