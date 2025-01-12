using System.ComponentModel.DataAnnotations.Schema;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities;

public abstract class EntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset LastModified { get; set; } = DateTimeOffset.UtcNow;
    public Guid? LastModifiedBy { get; set; }
    
    private readonly List<EventBase> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<EventBase> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(EventBase domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(EventBase domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}