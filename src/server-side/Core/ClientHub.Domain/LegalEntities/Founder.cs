using ClientHub.Domain.Clients;

namespace ClientHub.Domain.LegalEntities;

public sealed class FounderClient : LegalEntityLink
{
    public FounderClient(
        LegalEntityLinkType type,
        DateTime assignedAt,
        DateTime? disassignedAt,
        decimal incomeShareInDollars,
        decimal incomeShareInPercent,
        Client client
    )
        : base(type, assignedAt, disassignedAt, incomeShareInDollars, incomeShareInPercent)
    {
        ArgumentNullException.ThrowIfNull(client);
        Client = client;
    }

    public Client Client { get; private set; }
}

public sealed class FounderPerson : LegalEntityLink
{
    public FounderPerson(
        LegalEntityLinkType type,
        DateTime assignedAt,
        DateTime? disassignedAt,
        decimal incomeShareInDollars,
        decimal incomeShareInPercent,
        Person person
    )
        : base(type, assignedAt, disassignedAt, incomeShareInDollars, incomeShareInPercent)
    {
        Person = person ?? throw new InvalidPropertyException(nameof(person));
    }

    public Person Person { get; private set; }
}
