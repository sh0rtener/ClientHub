using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClientHub.UseCases.LegalEntities;
using Microsoft.EntityFrameworkCore;

namespace ClientHub.Persistense.EntityFramework.Repositories;

public sealed class UseCaseLegalEntityRepository : IUseCaseLegalEntitiesRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public UseCaseLegalEntityRepository(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShortLegalEntityDto>> Get(
        GetShortLegalEntities filter,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(filter);

        if (filter.Limit <= 0)
            throw new ArgumentOutOfRangeException(nameof(filter.Limit));

        if (filter.Offset < 0)
            throw new ArgumentOutOfRangeException(nameof(filter.Offset));

        var query = _dbContext.LegalEntities.AsNoTracking().AsQueryable();

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
            var date = filter.RegistrationDate.Value.Date;
            var nextDay = date.AddDays(1);

            query = query.Where(x =>
                x.Client.RegistrationDate >= date && x.Client.RegistrationDate < nextDay
            );
        }

        if (filter.LiquidationDate.HasValue)
        {
            var date = filter.LiquidationDate.Value.Date;
            var nextDay = date.AddDays(1);

            query = query.Where(x =>
                x.Client.LiquidationDate >= date && x.Client.LiquidationDate < nextDay
            );
        }

        return await query
            .OrderBy(x => x.Id)
            .Skip(filter.Offset)
            .Take(filter.Limit)
            .Select(x => new ShortLegalEntityDto()
            {
                Id = x.Id,
                Inn = x.Client.Inn,
                Ogrn = x.Client.Ogrn,
                Region = x.Client.Region,
                Address = x.Client.Address,
                ClientStatus = x.Client.Status,
                RegistrationDate = x.Client.RegistrationDate,
                LiquidationDate = x.Client.LiquidationDate,
            })
            .ToListAsync(cancellationToken);
    }
}
