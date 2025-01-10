using AutoMapper;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Trips.Queries;

public record TripDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public IReadOnlyCollection<string> Participants { get; init; } = Array.Empty<string>();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Trip, TripDto>()
                .ForMember(t => t.Participants, opt => opt.MapFrom(t => t.Participants.Select(p => p.User.Name)));
        }
    }
}