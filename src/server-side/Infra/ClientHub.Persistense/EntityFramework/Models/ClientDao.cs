namespace ClientHub.Persistense.EntityFramework.Models;

public sealed class ClientDao
{
    public int Id { get; set; }
    public string Inn { get; set; } = string.Empty;
    public string Name { get; set; }
    public string Ogrn { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Status { get; set; }
    public GoodsMarkDao GoodsMark { get; set; } = new();
    public DateTime? RegistrationDate { get; set; }
    public DateTime? LiquidationDate { get; set; }
    public List<ClientContactDao> ClientContacts { get; set; } = [];
    public List<ClientActionDao> ClientActions { get; set; } = [];
    public List<ClientEventDao> ClientEvents { get; set; } = [];
    public List<ArbitrageDealDao> ArbitrageDeals { get; set; } = [];
    public List<EnforcementProceedingsDao> Proceedings { get; set; } = [];
    public List<CourtsOfGeneralJurisdictionDao> Jurisdictions { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}