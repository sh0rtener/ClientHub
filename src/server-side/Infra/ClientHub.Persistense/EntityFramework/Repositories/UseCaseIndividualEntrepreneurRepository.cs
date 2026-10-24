using ClientHub.UseCases.IndividualEntrepreneurs;
using Microsoft.EntityFrameworkCore;

namespace ClientHub.Persistense.EntityFramework.Repositories;

public sealed class UseCaseIndividualEntrepreneurRepository
    : IUseCaseIndividualEntrepreneursRepository
{
    private readonly AppDbContext _dbContext;

    public UseCaseIndividualEntrepreneurRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ShortIndividualEntrepreneursDto>> Get(
        GetShortIndividualEntrepreneurs filter,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(filter);

        if (filter.Limit <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(filter.Limit),
                "Limit должен быть больше нуля."
            );

        if (filter.Offset < 0)
            throw new ArgumentOutOfRangeException(
                nameof(filter.Offset),
                "Offset не может быть отрицательным."
            );

        var query = _dbContext.IndividualEntrepreneurs.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Inn))
        {
            var inn = filter.Inn.Trim();

            query = query.Where(x => x.Client.Inn.Contains(inn));
        }

        if (!string.IsNullOrWhiteSpace(filter.Ogrn))
        {
            var ogrn = filter.Ogrn.Trim();

            query = query.Where(x => x.Client.Ogrn.Contains(ogrn));
        }

        if (!string.IsNullOrWhiteSpace(filter.Region))
        {
            var region = filter.Region.Trim();

            query = query.Where(x => x.Client.Region.Contains(region));
        }

        if (filter.Status.HasValue)
        {
            query = query.Where(x => x.Client.Status == filter.Status.Value);
        }

        if (filter.RegistrationDate.HasValue)
        {
            var registrationDate = filter.RegistrationDate.Value.Date;
            var nextDay = registrationDate.AddDays(1);

            query = query.Where(x =>
                x.Client.RegistrationDate >= registrationDate && x.Client.RegistrationDate < nextDay
            );
        }

        if (filter.LiquidationDate.HasValue)
        {
            var liquidationDate = filter.LiquidationDate.Value.Date;
            var nextDay = liquidationDate.AddDays(1);

            query = query.Where(x =>
                x.Client.LiquidationDate >= liquidationDate && x.Client.LiquidationDate < nextDay
            );
        }

        return await query
            .OrderBy(x => x.Id)
            .Skip(filter.Offset)
            .Take(filter.Limit)
            .Select(x => new ShortIndividualEntrepreneursDto
            {
                Id = x.Id,
                Inn = x.Client.Inn,
                Ogrn = x.Client.Ogrn,
                Region = x.Client.Region,
                Address = x.Client.Address,
                ClientStatus = x.Client.Status,
                RegistrationDate = x.Client.RegistrationDate,
                LiquidationDate = x.Client.LiquidationDate,
                Requisite = new IndividualEntrepreneurRequisiteDto
                {
                    Id = x.Requisite.Id,
                    Kind = x.Requisite.Kind,
                    Registrator = x.Requisite.Registrator,
                    TaxAuthority = x.Requisite.TaxAuthority,
                    RegistrationDate = x.Requisite.RegistrationDate,
                    TaxRegistrationDate = x.Requisite.TaxRegistrationDate,
                },
            })
            .ToListAsync(cancellationToken);
    }
}
