using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TripPlanner.Application.Common.Interfaces;

namespace TripPlanner.Application.Trips.Queries;

// [Authorize]
public class GetTripsQuery : IRequest<IReadOnlyCollection<TripDto>>
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
    private readonly IMapper _mapper;
    private readonly Guid _userId;

    public GetTripsQueryHandler(IApplicationDbContext context, IMapper mapper, IUser user)
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
            .Where(t => t.Participants.Any(p => p.Id == _userId))
            .ProjectTo<TripDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken: cancellationToken);
        
        return trips;
    }
}