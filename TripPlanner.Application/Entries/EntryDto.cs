using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Core.Entities;
using TripPlanner.Core.Enums;

namespace TripPlanner.Application.Entries;

public record EntryDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Comment { get; init; }
    public DateTime Created { get; init; }
    public string CreatedBy { get; init; } = string.Empty;
    public Guid TripId { get; init; }

    public Dictionary<ReactionType, int> Reactions { get; init; } = [];
    public IReadOnlyCollection<ReactionType> UserOwnReactions { get; init; } = [];
}

public class EntryMapper(IUser? user) : IMapper<Entry, EntryDto>
{
    public EntryDto Map(Entry entry)
    {
        return new EntryDto
        {
            Id = entry.Id,
            Title = entry.Title,
            Comment = entry.Comment,
            Created = entry.Created,
            CreatedBy = entry.User.Name,
            TripId = entry.TripId,
            Reactions = entry.Reactions.CountBy(r => r.Type).ToDictionary(),
            UserOwnReactions = user != null
                ? entry.Reactions.Where(r => r.User.Id == user?.Id).Select(r => r.Type).ToArray()
                : []
        };
    }
}