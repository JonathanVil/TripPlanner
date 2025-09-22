using Ardalis.GuardClauses;

namespace TripPlanner.Core.Entities;

public class Trip(string title, DateTime startDate, DateTime endDate) : EntityBase
{
    public string Title { get; set; } = Guard.Against.NullOrEmpty(title, nameof(title));
    public List<Participation> Participants { get; set; } = new();
    public List<Entry> Entries { get; set; } = new();
    public string JoinCode { get; set; } = RandomString(6);
    public DateTime StartDate { get; set; } = Guard.Against.Default(startDate, nameof(startDate));
    public DateTime EndDate { get; set; } = Guard.Against.Default(endDate, nameof(endDate));

    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
    }
}