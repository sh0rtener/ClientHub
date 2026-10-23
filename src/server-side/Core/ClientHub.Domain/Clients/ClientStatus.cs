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

    public static ClientStatus DefineStatus(int id)
    {
        switch (id)
        {
            case 1:
                return ClientStatus.Active;
                break;
            case 2:
                return ClientStatus.PendingTermination;
                break;
            case 3:
                return ClientStatus.Terminated;
                break;
            case 4:
                return ClientStatus.PendingRegistration;
                break;
            default:
                throw new InvalidDataException(
                    "По заданным параметрам не удалось определить статус клиента"
                );
        }
    }
}
