namespace ClientHub.Domain.Clients;

public sealed class ClientEvent : Entity<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ClientSource Source { get; private set; }
    public DateTime EventedAt { get; private set; }

    public ClientEvent(string name, string description, ClientSource source, DateTime eventedAt)
    {
        Guard.ValidateLength(name);
        Guard.ValidateLength(description);

        Name = name.Trim();
        Description = description.Trim();
        Source = source ?? throw new InvalidPropertyException(nameof(source));
        EventedAt = eventedAt;
    }
}
