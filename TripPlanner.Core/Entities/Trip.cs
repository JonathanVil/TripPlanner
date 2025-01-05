using Ardalis.GuardClauses;

namespace TripPlanner.Core.Entities;

public class Trip : EntityBase
{
    public Trip(string title, ApplicationUser owner)
    {
        Title = Guard.Against.NullOrEmpty(title, nameof(title));
        Owner = Guard.Against.Null(owner, nameof(owner));
        Participants.Add(owner);
    }
    
    public string Title { get; set; }
    public ApplicationUser Owner { get; set; }
    public List<ApplicationUser> Participants { get; set; } = new();
}