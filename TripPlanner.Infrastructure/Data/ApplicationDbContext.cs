using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Core.Entities;

namespace TripPlanner.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<Entry> Entries => Set<Entry>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}