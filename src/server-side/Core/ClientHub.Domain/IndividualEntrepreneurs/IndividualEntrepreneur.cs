using ClientHub.Domain.Clients;
using ClientHub.Domain.Clients.Arbitrages;
using ClientHub.Domain.Clients.Courts;
using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.Clients.Enforcements;

namespace ClientHub.Domain.IndividualEntrepreneurs;

public sealed class IndividualEntrepreneur : Client
{
    public IndividualEntrepreneur(
        Inn inn,
        string name,
        Ogrn ogrn,
        string region,
        string address,
        ClientStatus status,
        GoodsMark goodsMark,
        DateTime? registrationDate,
        DateTime? liquidationDate,
        IEnumerable<ClientContact>? contacts,
        IEnumerable<ClientAction>? actions,
        IEnumerable<ClientEvent>? events,
        IEnumerable<ArbitrageDeal>? arbitragesDeals,
        IEnumerable<EnforcementProceedings>? proceedings,
        IEnumerable<CourtsOfGeneralJurisdiction>? jurisdictions,
        IndividualEntrepreneurRequisite requisite
    )
        : base(
            inn,
            name,
            ogrn,
            region,
            address,
            ClientType.IndividualEntrepreneurs,
            status,
            goodsMark,
            registrationDate,
            liquidationDate,
            contacts,
            actions,
            events,
            arbitragesDeals,
            proceedings,
            jurisdictions
        )
    {
        Requisite = requisite ?? throw new InvalidPropertyException((nameof(requisite)));
    }

    public IndividualEntrepreneurRequisite Requisite { get; private set; }
}
