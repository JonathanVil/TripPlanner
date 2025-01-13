using AutoMapper;
using TripPlanner.Application.Entries;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Trips;

public record TripDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public IReadOnlyCollection<ParticipationDto> Participants { get; init; } = Array.Empty<ParticipationDto>();
    public IReadOnlyCollection<EntryDto> Entries { get; init; } = Array.Empty<EntryDto>();
    public string JoinCode { get; init; } = string.Empty;
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Trip, TripDto>();
        }
    }
}