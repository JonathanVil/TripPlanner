using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Core.Entities;
using TripPlanner.Core.Enums;

namespace TripPlanner.Application.Reactions.Commands;

public record ToggleReactionCommand(Guid EntryId, ReactionType Type) : IRequest<Guid>;

public class ToggleReactionCommandValidator : AbstractValidator<ToggleReactionCommand>
{
    public ToggleReactionCommandValidator()
    {
        RuleFor(x => x).NotNull();
    }
}

public class ToggleReactionCommandHandler : IRequestHandler<ToggleReactionCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly string _userId;
    
    public ToggleReactionCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _userId = Guard.Against.Null(user.Id);
    }

    public async Task<Guid> Handle(ToggleReactionCommand request, CancellationToken cancellationToken)
    {
        var entry = await _context.Entries.FindAsync([request.EntryId], cancellationToken);

        if (entry == null)
        {
            throw new NotFoundException(nameof(Entry), request.EntryId.ToString());
        }

        var user = await _context.Users.FindAsync([_userId], cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), _userId);
        }

        var reaction = new Reaction
        {
            Type = request.Type,
            User = user,
            Entry = entry
        };
        
        // toggle the reaction
        var existingReaction = await _context.Reactions
            .FirstOrDefaultAsync(r => r.UserId == _userId && r.EntryId == request.EntryId && r.Type == request.Type, cancellationToken);
        
        if (existingReaction != null)
        {
            _context.Reactions.Remove(existingReaction);
        }
        else
        {
            _context.Reactions.Add(reaction);
        }
        
        await _context.SaveChangesAsync(cancellationToken);

        return entry.Id;
    }
}