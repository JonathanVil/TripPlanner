using System.ComponentModel.DataAnnotations.Schema;

namespace TripPlanner.Core.Common;

public abstract class EntityBase
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    
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