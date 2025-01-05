using Microsoft.AspNetCore.Identity;

namespace TripPlanner.Core.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public List<Trip> Trips { get; set; } = new();
}