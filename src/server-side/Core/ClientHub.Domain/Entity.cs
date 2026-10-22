namespace ClientHub.Domain;

public abstract class Entity<TId>
{
    public TId? Id { get; set; }

    protected virtual void OnEntityUpdated() { }
    // тут можно завернуть в будущем под событийный подход
    // мое мнение - на данном этапе мне это здесь не нужно,
    // нужно на самостоятельность и инициативность смотреть...
}

public abstract class AuditableEntity<TId> : Entity<TId>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    protected override void OnEntityUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
