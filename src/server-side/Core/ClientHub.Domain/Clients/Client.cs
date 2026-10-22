using System.Text.RegularExpressions;
using ClientHub.Domain.Clients.Arbitrages;
using ClientHub.Domain.Clients.Courts;
using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.Clients.Enforcements;
using ClientHub.Domain.Clients.Exceptions;

namespace ClientHub.Domain.Clients;

public abstract class Client : AuditableEntity<int>
{
    private List<ClientContact> _contacts;
    private List<ClientAction> _actions;
    private List<ClientEvent> _events;
    private List<ArbitrageDeal> _arbitragesDeals;
    private List<EnforcementProceedings> _proceedings;
    private List<CourtsOfGeneralJurisdiction> _jurisdictions;

    public Inn Inn { get; private set; }
    public string Name { get; private set; }
    public Ogrn Ogrn { get; private set; }
    public string Region { get; private set; }
    public string Address { get; private set; }

    public ClientType Type { get; private set; }
    public ClientStatus Status { get; private set; }
    public GoodsMark GoodsMark { get; private set; }

    public IReadOnlyCollection<ClientContact> Contacts => _contacts.AsReadOnly();
    public IReadOnlyCollection<ClientAction> Actions => _actions.AsReadOnly();
    public IReadOnlyCollection<ClientEvent> Events => _events.AsReadOnly();
    public IReadOnlyCollection<ArbitrageDeal> ArbitragesDeals => _arbitragesDeals.AsReadOnly();
    public IReadOnlyCollection<EnforcementProceedings> Proceedings => _proceedings.AsReadOnly();
    public IReadOnlyCollection<CourtsOfGeneralJurisdiction> Jurisdictions =>
        _jurisdictions.AsReadOnly();

    public DateTime? RegistrationDate { get; private set; }
    public DateTime? LiquidationDate { get; private set; }

    public Client(
        Inn inn,
        string name,
        Ogrn ogrn,
        string region,
        string address,
        ClientType type,
        ClientStatus status,
        GoodsMark goodsMark,
        DateTime? registrationDate,
        DateTime? liquidationDate,
        IEnumerable<ClientContact>? contacts,
        IEnumerable<ClientAction>? actions,
        IEnumerable<ClientEvent>? events,
        IEnumerable<ArbitrageDeal>? arbitragesDeals,
        IEnumerable<EnforcementProceedings>? proceedings,
        IEnumerable<CourtsOfGeneralJurisdiction>? jurisdictions
    )
    {
        Guard.ValidateLength(name);
        Name = name.Trim();
        Inn = inn ?? throw new InvalidPropertyException(nameof(inn));
        Ogrn = ogrn ?? throw new InvalidPropertyException(nameof(ogrn));
        Guard.ValidateLength(region);
        Region = region.Trim();
        Guard.ValidateLength(address);
        Address = address.Trim();

        Type = type ?? throw new InvalidPropertyException(nameof(type));
        Status = status ?? throw new InvalidPropertyException(nameof(status));
        GoodsMark = goodsMark ?? throw new InvalidPropertyException(nameof(goodsMark));
        RegistrationDate = registrationDate;
        LiquidationDate = liquidationDate;
        _contacts = contacts?.ToList() ?? [];
        _actions = actions?.ToList() ?? [];
        _events = events?.ToList() ?? [];
        _arbitragesDeals = arbitragesDeals?.ToList() ?? [];
        _proceedings = proceedings?.ToList() ?? [];
        _jurisdictions = jurisdictions?.ToList() ?? [];
    }

    public void ChangeInn(Inn inn) => Inn = inn ?? throw new InvalidPropertyException(nameof(inn));

    public void ChangeName(string name)
    {
        Guard.ValidateLength(name);
        Name = name.Trim();
    }

    public void ChangeOgrn(Ogrn ogrn) =>
        Ogrn = ogrn ?? throw new InvalidPropertyException(nameof(ogrn));

    public void ChangeRegion(string region)
    {
        Guard.ValidateLength(region);
        Region = region.Trim();
    }

    public void ChangeAddress(string address)
    {
        Guard.ValidateLength(address);
        Address = address.Trim();
    }

    public void SetActiveStatus(DateTime registrationDate)
    {
        if (!Status.Equals(ClientStatus.PendingRegistration))
            throw new CannotChangeClientStatusException();

        RegistrationDate = registrationDate;
        Status = ClientStatus.Active;
    }

    public void SetPendingTermination()
    {
        if (!Status.Equals(ClientStatus.Active))
            throw new CannotChangeClientStatusException();

        Status = ClientStatus.PendingTermination;
    }

    public void SetTerminated(DateTime liquidationDate)
    {
        if (Status.Equals(ClientStatus.PendingRegistration))
            throw new CannotChangeClientStatusException();

        LiquidationDate = liquidationDate;
        Status = ClientStatus.Terminated;
    }

    public void ChangeGoodsMark(GoodsMark goodsMark) =>
        GoodsMark = goodsMark ?? throw new InvalidPropertyException(nameof(goodsMark));

    public void AddContacts(IEnumerable<ClientContact> contacts) =>
        Guard.SaveAddToCollection(_contacts, contacts);

    public void RemoveContacts(IEnumerable<ClientContact> contacts) =>
        Guard.SaveRemoveToCollection(_contacts, contacts);

    public void ClearContacts() => _contacts.Clear();

    public void AddActions(IEnumerable<ClientAction> values) =>
        Guard.SaveAddToCollection(_actions, values);

    public void RemoveActions(IEnumerable<ClientAction> values) =>
        Guard.SaveRemoveToCollection(_actions, values);

    public void ClearActions() => _actions.Clear();

    public void AddEvents(IEnumerable<ClientEvent> values) =>
        Guard.SaveAddToCollection(_events, values);

    public void RemoveEvents(IEnumerable<ClientEvent> values) =>
        Guard.SaveRemoveToCollection(_events, values);

    public void ClearEvents() => _events.Clear();

    public void AddArbitrages(IEnumerable<ArbitrageDeal> values) =>
        Guard.SaveAddToCollection(_arbitragesDeals, values);

    public void RemoveArbitrages(IEnumerable<ArbitrageDeal> values) =>
        Guard.SaveRemoveToCollection(_arbitragesDeals, values);

    public void ClearArbitrages() => _arbitragesDeals.Clear();

    public void AddProceedings(IEnumerable<EnforcementProceedings> values) =>
        Guard.SaveAddToCollection(_proceedings, values);

    public void RemoveProceedings(IEnumerable<EnforcementProceedings> values) =>
        Guard.SaveRemoveToCollection(_proceedings, values);

    public void ClearProceedings() => _proceedings.Clear();

    public void AddJurisdictions(IEnumerable<CourtsOfGeneralJurisdiction> values) =>
        Guard.SaveAddToCollection(_jurisdictions, values);

    public void RemoveJurisdictions(IEnumerable<CourtsOfGeneralJurisdiction> values) =>
        Guard.SaveRemoveToCollection(_jurisdictions, values);

    public void ClearJurisdictions() => _jurisdictions.Clear();
}
