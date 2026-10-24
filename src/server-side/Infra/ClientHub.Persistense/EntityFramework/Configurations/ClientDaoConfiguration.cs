using ClientHub.Persistense.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientHub.Persistense.EntityFramework.Configurations;

public sealed class ClientDaoConfiguration : IEntityTypeConfiguration<ClientDao>
{
    public void Configure(EntityTypeBuilder<ClientDao> builder)
    {
        builder.HasMany(x => x.ClientContacts).WithOne().OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ClientActions).WithOne().OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ClientEvents).WithOne().OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ArbitrageDeals).WithOne().OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Proceedings).WithOne().OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Jurisdictions).WithOne().OnDelete(DeleteBehavior.Cascade);
    }
}
