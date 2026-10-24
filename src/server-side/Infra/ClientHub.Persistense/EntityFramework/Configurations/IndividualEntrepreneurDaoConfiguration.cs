using ClientHub.Persistense.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientHub.Persistense.EntityFramework.Configurations;

public sealed class IndividualEntrepreneurDaoConfiguration
    : IEntityTypeConfiguration<IndividualEntrepreneursDao>
{
    public void Configure(EntityTypeBuilder<IndividualEntrepreneursDao> builder)
    {
        builder
            .HasOne(x => x.Client)
            .WithOne()
            .HasForeignKey<IndividualEntrepreneursDao>(x => x.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Requisite)
            .WithOne()
            .HasForeignKey<IndividualEntrepreneursDao>(x => x.RequisiteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
