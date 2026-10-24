using ClientHub.Persistense.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientHub.Persistense.EntityFramework;

public sealed class AppDbContext : DbContext
{
    public DbSet<ArbitrageDealDao> ArbitrageDeals => Set<ArbitrageDealDao>();
    public DbSet<BeneficiarDao> Beneficiars => Set<BeneficiarDao>();
    public DbSet<ClientActionDao> ClientActions => Set<ClientActionDao>();
    public DbSet<ClientContactDao> ClientContacts => Set<ClientContactDao>();
    public DbSet<ClientDao> Clients => Set<ClientDao>();
    public DbSet<ClientEventDao> ClientEvents => Set<ClientEventDao>();
    public DbSet<CourtsOfGeneralJurisdictionDao> Courts => Set<CourtsOfGeneralJurisdictionDao>();
    public DbSet<EnforcementProceedingsDao> Enforcements => Set<EnforcementProceedingsDao>();
    public DbSet<FounderClientDao> FounderClients => Set<FounderClientDao>();
    public DbSet<FounderPersonDao> FounderPersons => Set<FounderPersonDao>();
    public DbSet<GoodsMarkDao> GoodsMarks => Set<GoodsMarkDao>();
    public DbSet<IndividualEntrepreneurRequisiteDao> IndividualEntrepreneurRequisites =>
        Set<IndividualEntrepreneurRequisiteDao>();
    public DbSet<IndividualEntrepreneursDao> IndividualEntrepreneurs =>
        Set<IndividualEntrepreneursDao>();
    public DbSet<KppDao> Kpps => Set<KppDao>();
    public DbSet<LegalEntityDao> LegalEntities => Set<LegalEntityDao>();
    public DbSet<MaintainerDao> Maintainers => Set<MaintainerDao>();
    public DbSet<PersonDao> PersonDaos => Set<PersonDao>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
