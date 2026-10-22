using ClientHub.Domain.Clients;

namespace ClientHub.Domain.LegalEntities;

public sealed class Maintainer : LegalEntityLink
{
    public Maintainer(
        LegalEntityLinkType type,
        DateTime assignedAt,
        DateTime? disassignedAt,
        decimal incomeShareInDollars,
        decimal incomeShareInPercent,
        Person person
    )
        : base(type, assignedAt, disassignedAt, incomeShareInDollars, incomeShareInPercent)
    {
        Person = person ?? throw new InvalidOperationException(nameof(person));
    }

    public Person Person { get; private set; }
}
