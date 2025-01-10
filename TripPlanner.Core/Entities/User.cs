
namespace TripPlanner.Core.Entities;

public class User : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public List<Participation> Participations { get; set; } = new();
}