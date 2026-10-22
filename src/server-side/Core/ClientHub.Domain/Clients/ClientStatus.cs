namespace ClientHub.Domain.Clients;

public sealed class ClientStatus : ValueObject
{
    public int Identifier { get; }

    public static ClientStatus Active => new(1);
    public static ClientStatus PendingTermination => new(2);
    public static ClientStatus Terminated => new(3);
    public static ClientStatus PendingRegistration => new(4);

    private ClientStatus(int id)
    {
        Identifier = id;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Identifier;
    }
}
