using NerdStoreEnterprise.Core.Messages;

namespace NerdStoreEnterprise.Core.DomainObjects;

public abstract class Entity
{
    private List<Event> _notificacoes = [];
    public IReadOnlyCollection<Event> Notifications => _notificacoes.AsReadOnly();


    public Guid Id { get; protected set; }
    
    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }

    

    public void AddEvent(Event eventDomain)
    {
        _notificacoes = _notificacoes ?? [];
        _notificacoes.Add(eventDomain);
    }

    public void RemoveEvent(Event eventItem)
    {
        _notificacoes?.Remove(eventItem);
    }

    public void ClearEvents()
    {
        _notificacoes?.Clear();
    }
}