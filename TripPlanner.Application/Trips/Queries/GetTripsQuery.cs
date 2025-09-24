using System.Collections.Immutable;
using Ardalis.GuardClauses;
using FluentValidation;
using TripPlanner.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Trips.Queries;

[Authorize]
public record GetTripsQuery : IRequest<IReadOnlyCollection<TripDto>>
{
}

public class GetTripsQueryValidator : AbstractValidator<GetTripsQuery>
{
    public GetTripsQueryValidator()
    {
    }
}

public class GetTripsQueryHandler : IRequestHandler<GetTripsQuery, IReadOnlyCollection<TripDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper<Trip, TripDto> _mapper;
    private readonly string _userId;

    public GetTripsQueryHandler(IApplicationDbContext context, IMapper<Trip, TripDto> mapper, IUser user)
    {
        _context = context;
        _mapper = mapper;
        _userId = Guard.Against.Null(user.Id);
    }

    public async Task<IReadOnlyCollection<TripDto>> Handle(GetTripsQuery request, CancellationToken cancellationToken)
    {
        var trips = await _context.Trips
            .AsNoTracking()
            .Include(t => t.Participants)
            .Where(t => t.Participants.Any(p => p.UserId == _userId))
            .ToListAsync(cancellationToken: cancellationToken);

        return _mapper.Map(trips).ToImmutableList();
    }
}