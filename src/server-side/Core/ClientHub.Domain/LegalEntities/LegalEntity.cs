using ClientHub.Domain.Clients;
using ClientHub.Domain.Clients.Arbitrages;
using ClientHub.Domain.Clients.Courts;
using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.Clients.Enforcements;

namespace ClientHub.Domain.LegalEntities;

public sealed class LegalEntity : Client
{
    private const int _minHeadCount = 0;
    private const int _maxHeadCount = 1_000_000;

    private List<FounderClient> _founderClients = [];
    private List<FounderPerson> _founderPersons = [];
    private List<Beneficiar> _beneficiars = [];
    private List<Maintainer> _maintainers = [];
    private List<Kpp> _kpps = [];

    public LegalEntity(
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
        Okato okatoCode,
        int headCount
    )
        : base(
            inn,
            name,
            ogrn,
            region,
            address,
            ClientType.LegalEntity,
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
        OkatoCode = okatoCode ?? throw new InvalidPropertyException(nameof(okatoCode));
        ValidateHeadCounts(headCount);
        HeadCount = headCount;
    }

    public Okato OkatoCode { get; private set; }
    public int HeadCount { get; private set; }

    public void ChangeOkatoCode(Okato okatoCode) =>
        OkatoCode = okatoCode ?? throw new InvalidPropertyException(nameof(okatoCode));

    public void ChangeHeadCount(int headCount)
    {
        ValidateHeadCounts(headCount);
        HeadCount = headCount;
    }

    public IReadOnlyCollection<FounderClient> FounderClients => _founderClients.AsReadOnly();
    public IReadOnlyCollection<FounderPerson> FounderPersons => _founderPersons.AsReadOnly();
    public IReadOnlyCollection<Beneficiar> Beneficiars => _beneficiars.AsReadOnly();
    public IReadOnlyCollection<Maintainer> Maintainers => _maintainers.AsReadOnly();
    public IReadOnlyCollection<Kpp> Kpps => _kpps.AsReadOnly();

    public void AddFounderClients(IEnumerable<FounderClient> founderClients) =>
        Guard.SaveAddToCollection(_founderClients, founderClients);

    public void AddFounderPersons(IEnumerable<FounderPerson> founderPersons) =>
        Guard.SaveAddToCollection(_founderPersons, founderPersons);

    public void AddBeneficiaries(IEnumerable<Beneficiar> beneficiars) =>
        Guard.SaveAddToCollection(_beneficiars, beneficiars);

    public void AddMaintainers(IEnumerable<Maintainer> maintainers) =>
        Guard.SaveAddToCollection(_maintainers, maintainers);

    public void AddKpps(IEnumerable<Kpp> kpps) => Guard.SaveAddToCollection(_kpps, kpps);

    public void RemoveFounderClients(IEnumerable<FounderClient> founderClients) =>
        Guard.SaveRemoveToCollection(_founderClients, founderClients);

    public void RemoveFounderPersons(IEnumerable<FounderPerson> founderPersons) =>
        Guard.SaveRemoveToCollection(_founderPersons, founderPersons);

    public void RemoveBeneficiaries(IEnumerable<Beneficiar> beneficiars) =>
        Guard.SaveRemoveToCollection(_beneficiars, beneficiars);

    public void RemoveMaintainers(IEnumerable<Maintainer> maintainers) =>
        Guard.SaveRemoveToCollection(_maintainers, maintainers);

    public void RemoveKpps(IEnumerable<Kpp> kpps) => Guard.SaveRemoveToCollection(_kpps, kpps);

    public void ClearFounderClients() => _founderClients.Clear();

    public void ClearFounderPersons() => _founderPersons.Clear();

    public void ClearBeneficiaries() => _beneficiars.Clear();

    public void ClearMaintainers() => _maintainers.Clear();

    public void ClearKpps() => _kpps.Clear();

    private void ValidateHeadCounts(int headCount)
    {
        if (headCount < _minHeadCount)
            throw new InvalidPropertyException(
                nameof(headCount),
                $"не может быть меньше {_minHeadCount}"
            );
        if (headCount > _maxHeadCount)
            throw new InvalidPropertyException(
                nameof(headCount),
                $"не может превышать {_maxHeadCount}"
            );
    }
}
