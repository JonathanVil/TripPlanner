using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Trips.Queries;

public record GetTripSummaryQuery(string JoinCode) : IRequest<TripSummaryDto?>
{
    public string JoinCode { get; init; } = JoinCode.ToUpper();
}

public class GetTripSummaryQueryValidator : AbstractValidator<GetTripSummaryQuery>
{
    public GetTripSummaryQueryValidator()
    {
        RuleFor(x => x.JoinCode).NotEmpty();
    }
}

public class GetTripSummaryQueryHandler : IRequestHandler<GetTripSummaryQuery, TripSummaryDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper<Trip, TripSummaryDto> _mapper;

    public GetTripSummaryQueryHandler(IApplicationDbContext context, IMapper<Trip, TripSummaryDto> mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TripSummaryDto?> Handle(GetTripSummaryQuery request, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .AsNoTracking()
            .Include(t => t.Participants)
            .FirstOrDefaultAsync(t => t.JoinCode == request.JoinCode, cancellationToken: cancellationToken);

        if (trip == null) return null;
        
        return _mapper.Map(trip);
    }
}