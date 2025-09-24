using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Application.Entries;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Trips;

public record TripDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public List<ParticipationDto> Participants { get; init; } = [];
    public List<EntryDto> Entries { get; init; } = [];
    public string JoinCode { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}

public class TripMapper(IMapper<Entry, EntryDto> entryMapper, IMapper<Participation, ParticipationDto> participationMapper) : IMapper<Trip, TripDto>
{
    public TripDto Map(Trip from)
    {
        return new TripDto
        {
            Id = from.Id,
            Title = from.Title,
            Participants = from.Participants.Select(participationMapper.Map).ToList(),
            Entries = from.Entries.Select(entryMapper.Map).ToList(),
            JoinCode = from.JoinCode,
            StartDate = from.StartDate,
            EndDate = from.EndDate
        };
    }
}