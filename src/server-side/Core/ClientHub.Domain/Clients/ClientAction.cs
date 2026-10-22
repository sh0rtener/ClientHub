namespace ClientHub.Domain.Clients;

public enum ClientActionType
{
    Primary,
    Secondary,
}

public sealed class ClientAction : Entity<Guid>
{
    public string Name { get; private set; }
    public ClientActionType Kind { get; private set; }
    public string Code { get; private set; }

    public ClientAction(string name, string code, ClientActionType kind)
    {
        Guard.ValidateLength(name);
        Guard.ValidateLength(code);
        Name = name.Trim();
        Code = code.Trim();
        Kind = kind;
    }
}
