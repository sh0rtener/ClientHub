using AutoMapper;
using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.LegalEntities;
using ClientHub.Persistense.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientHub.Persistense.EntityFramework.Repositories;

public sealed class LegalEntityRepository : ILegalEntityRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public LegalEntityRepository(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<LegalEntity?> Get(int id, CancellationToken cancellationToken = default)
    {
        var dao = await _dbContext
            .LegalEntities.AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Client)
                .ThenInclude(x => x.ClientContacts)
            .Include(x => x.Client)
                .ThenInclude(x => x.ClientActions)
            .Include(x => x.Client)
                .ThenInclude(x => x.ClientEvents)
            .Include(x => x.Client)
                .ThenInclude(x => x.ArbitrageDeals)
            .Include(x => x.Client)
                .ThenInclude(x => x.Proceedings)
            .Include(x => x.Client)
                .ThenInclude(x => x.Jurisdictions)
            .Include(x => x.FounderClients)
            .Include(x => x.FounderPersons)
            .Include(x => x.Beneficiars)
            .Include(x => x.Maintainers)
            .Include(x => x.Kpps)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return dao is null ? null : LegalEntityDao.ToDomain(dao, _mapper);
    }

    public Task<bool> IsExist(int id, CancellationToken cancellationToken = default)
    {
        return _dbContext.LegalEntities.AnyAsync(x => x.ClientId == id, cancellationToken);
    }

    public Task<bool> IsExist(Inn inn, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(inn);

        return _dbContext.LegalEntities.AnyAsync(x => x.Client.Inn == inn.Value, cancellationToken);
    }

    public async Task<int> Add(LegalEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var dao = _mapper.Map<LegalEntityDao>(entity);

        await _dbContext.LegalEntities.AddAsync(dao, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return dao.Id;
    }

    public async Task<LegalEntity> Update(
        int id,
        LegalEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(entity);

        var dao = await _dbContext
            .LegalEntities.Include(x => x.Client)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        _mapper.Map(entity, dao);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return LegalEntityDao.ToDomain(dao, _mapper);
    }

    public async Task Remove(int id, CancellationToken cancellationToken = default)
    {
        var leDao = await _dbContext.LegalEntities.FirstOrDefaultAsync(
            x => x.Id == id,
            cancellationToken
        );

        if (leDao is null)
            return;

        var dao = await _dbContext.Clients.FirstOrDefaultAsync(
            x => x.Id == leDao.ClientId,
            cancellationToken
        );

        if (dao is null)
            return;

        _dbContext.Clients.Remove(dao);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
