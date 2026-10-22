namespace ClientHub.Domain.Clients;

public sealed class GoodsMark : Entity<Guid>
{
    public string Name { get; private set; }
    public DateTime AssignedAt { get; private set; }
    public DateTime? DisassignedAt { get; private set; }

    public GoodsMark(string name, DateTime assignedAt, DateTime? disassignedAt)
    {
        Guard.ValidateLength(name);

        Name = name.Trim();
        AssignedAt = assignedAt;
        DisassignedAt = disassignedAt;
    }
}
