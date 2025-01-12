using AutoMapper;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Trips;

public record ParticipationDto
{
    public string UserName { get; init; } = string.Empty;
    public DateTimeOffset JoinedOn { get; init; }
    public bool IsOwner { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Participation, ParticipationDto>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.Name));
        }
    }
}