using Ardalis.GuardClauses;
using FluentValidation;
using Galerie.Application.Common.Exceptions;
using MediatR;
using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Entries.Commands;

public record CreateEntryCommand(Guid TripId) : IRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
}

public class CreateEntryCommandValidator : AbstractValidator<CreateEntryCommand>
{
    public CreateEntryCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();
    }
}

public class CreateEntryCommandHandler : IRequestHandler<CreateEntryCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly string _userId;

    public CreateEntryCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _userId = Guard.Against.Null(user.Id);
    }

    public async Task Handle(CreateEntryCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(_userId, cancellationToken);
        Guard.Against.Null(user, nameof(user));

        var trip = await _context.Trips.FindAsync(request.TripId, cancellationToken);

        Guard.Against.Null(trip, nameof(trip));
        if (trip.Participants.All(p => p.UserId != _userId))
        {
            throw new ForbiddenAccessException();
        }

        var entity = new Entry
        {
            Name = request.Name,
            Trip = trip,
            User = user
        };

        _context.Entries.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}