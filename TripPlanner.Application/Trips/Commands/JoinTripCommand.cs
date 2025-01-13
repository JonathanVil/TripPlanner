using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TripPlanner.Application.Common.Exceptions;
using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Trips.Commands;

public record JoinTripCommand(string JoinCode) : IRequest<Guid>
{
    public string JoinCode { get; init; } = JoinCode.ToUpper();
}

public class JoinTripCommandValidator : AbstractValidator<JoinTripCommand>
{
    public JoinTripCommandValidator()
    {
        RuleFor(x => x.JoinCode).NotEmpty();
    }
}

public class JoinTripCommandHandler : IRequestHandler<JoinTripCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly string _userId;

    public JoinTripCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _userId = Guard.Against.Null(user.Id);
    }

    public async Task<Guid> Handle(JoinTripCommand request, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .Include(t => t.Participants)
            .FirstOrDefaultAsync(t => t.JoinCode == request.JoinCode, cancellationToken: cancellationToken);

        if (trip == null)
        {
            throw new NotFoundException(nameof(Trip), request.JoinCode);
        }

        if (trip.Participants.Any(p => p.UserId == _userId))
        {
            throw new ForbiddenAccessException();
        }

        trip.Participants.Add(new Participation
        {
            UserId = _userId
        });

        await _context.SaveChangesAsync(cancellationToken);

        return trip.Id;
    }
}