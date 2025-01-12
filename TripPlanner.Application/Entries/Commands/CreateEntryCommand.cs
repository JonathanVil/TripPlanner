using Ardalis.GuardClauses;
using FluentValidation;
using TripPlanner.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Entries.Commands;

public record CreateEntryCommand(Guid TripId, string Title) : IRequest
{
    public string? Comment { get; set; }
}

public class CreateEntryCommandValidator : AbstractValidator<CreateEntryCommand>
{
    public CreateEntryCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(50)
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

        var trip = await _context.Trips
            .Include(t => t.Participants)
            .Include(t => t.Entries)
            .FirstOrDefaultAsync(t => t.Id == request.TripId, cancellationToken);

        Guard.Against.Null(trip, nameof(trip));
        if (trip.Participants.All(p => p.UserId != _userId))
        {
            throw new ForbiddenAccessException();
        }

        var entity = new Entry
        {
            Title = request.Title,
            Comment = request.Comment,
            Trip = trip,
            User = user
        };

        _context.Entries.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}