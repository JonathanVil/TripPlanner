
namespace TripPlanner.Core.Entities;

public class User
{
    public required string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Participation> Participations { get; set; } = new();
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
}