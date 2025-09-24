using Ardalis.GuardClauses;
using MediatR;
using TripPlanner.Application.Common.Exceptions;
using TripPlanner.Application.Common.Interfaces;

namespace TripPlanner.Application.Entries.Commands;

public record DeleteEntryCommand(Guid EntryId) : IRequest;

public class DeleteEntryCommandHandler : IRequestHandler<DeleteEntryCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly string _userId;

    public DeleteEntryCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _userId = Guard.Against.Null(user.Id);
    }
    
    public async Task Handle(DeleteEntryCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync([_userId], cancellationToken);
        Guard.Against.Null(user, nameof(user));
        
        var entry = await _context.Entries.FindAsync([request.EntryId], cancellationToken);
        Guard.Against.Null(entry, nameof(entry));

        if (entry.UserId != _userId)
        {
            throw new ForbiddenAccessException();
        }
        
        _context.Entries.Remove(entry);
        await _context.SaveChangesAsync(cancellationToken);
    }
}