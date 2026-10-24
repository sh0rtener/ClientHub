using AutoMapper;
using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.IndividualEntrepreneurs;
using ClientHub.Persistense.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientHub.Persistense.EntityFramework.Repositories;

public sealed class IndividualEntrepreneurRepository : IIndividualEntrepreneurRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public IndividualEntrepreneurRepository(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IndividualEntrepreneur?> Get(
        int id,
        CancellationToken cancellationToken = default
    )
    {
        var entity = await _dbContext
            .IndividualEntrepreneurs.AsNoTracking()
            .Include(x => x.Requisite)
            .Include(x => x.Client)
                .ThenInclude(x => x.GoodsMark)
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
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity is null ? null : IndividualEntrepreneursDao.ToDomain(entity, _mapper);
    }

    public Task<bool> IsExist(int id, CancellationToken cancellationToken = default)
    {
        return _dbContext.IndividualEntrepreneurs.AnyAsync(
            x => x.Id == id,
            cancellationToken
        );
    }

    public Task<bool> IsExist(Inn inn, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(inn);

        return _dbContext.IndividualEntrepreneurs.AnyAsync(
            x => x.Client.Inn == inn.Value,
            cancellationToken
        );
    }

    public async Task<int> Add(
        IndividualEntrepreneur entity,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(entity);

        var dao = _mapper.Map<IndividualEntrepreneursDao>(entity);

        await _dbContext.IndividualEntrepreneurs.AddAsync(dao, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return dao.Id;
    }

    public async Task<IndividualEntrepreneur> Update(
        int id,
        IndividualEntrepreneur entity,
        CancellationToken cancellationToken = default
    )
    {
        var dao = await _dbContext
            .IndividualEntrepreneurs.Include(x => x.Client)
            .Include(x => x.Requisite)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (dao is null)
            throw new KeyNotFoundException($"Individual entrepreneur with id {id} was not found.");

        _mapper.Map(entity, dao);
        _mapper.Map(entity, dao.Client);
        _mapper.Map(entity.Requisite, dao.Requisite);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return IndividualEntrepreneursDao.ToDomain(dao, _mapper);
    }

    public async Task Remove(int id, CancellationToken cancellationToken = default)
    {
        var ipDao = await _dbContext.IndividualEntrepreneurs.FirstOrDefaultAsync(
            x => x.Id == id,
            cancellationToken
        );

        if (ipDao is null)
            return;

        var dao = await _dbContext.Clients.FirstOrDefaultAsync(
            x => x.Id == ipDao.ClientId,
            cancellationToken
        );

        if (dao is null)
            return;

        _dbContext.Clients.Remove(dao);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
