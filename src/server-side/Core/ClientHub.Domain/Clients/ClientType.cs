namespace ClientHub.Domain.Clients;

public sealed class ClientType : ValueObject
{
    private const string IE = "ИП";
    private const string LE = "ЮЛ";

    private int _identifier;
    private string _name;

    public int Identifier => _identifier;
    public string Name => _name;

    public static ClientType IndividualEntrepreneurs => new(1, IE);
    public static ClientType LegalEntity => new(2, LE);

    private ClientType(int identifier, string name)
    {
        _identifier = identifier;
        _name = name;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Identifier;
        yield return Name;
    }
}
