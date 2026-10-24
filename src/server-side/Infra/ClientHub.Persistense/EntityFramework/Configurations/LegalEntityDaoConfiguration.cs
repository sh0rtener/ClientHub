using ClientHub.Persistense.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientHub.Persistense.EntityFramework.Configurations;

public sealed class LegalEntityDaoConfiguration : IEntityTypeConfiguration<LegalEntityDao>
{
    public void Configure(EntityTypeBuilder<LegalEntityDao> builder)
    {
        builder
            .HasOne(x => x.Client)
            .WithOne()
            .HasForeignKey<LegalEntityDao>(x => x.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.FounderClients).WithOne().OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.FounderPersons).WithOne().OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Beneficiars).WithOne().OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Maintainers).WithOne().OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Kpps).WithOne().OnDelete(DeleteBehavior.Cascade);
    }
}
