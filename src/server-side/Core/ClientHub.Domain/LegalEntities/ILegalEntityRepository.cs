namespace ClientHub.Domain.LegalEntities;

public interface ILegalEntityRepository
{
    /// <summary>
    /// Получение полной информации об ЮЛ
    /// </summary>
    /// <param name="id">Идентификатор ЮЛ</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Юридическое Лицо, если существует, иначе null</returns>
    Task<LegalEntity?> Get(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавление нового юридического лица
    /// </summary>
    /// <param name="entity">Юридическое лицо</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Идентификатор добавленной записи</returns>
    Task<int> Add(LegalEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить сведения об ЮЛ
    /// </summary>
    /// <param name="id">Идентификатор обновляемой ЮЛ</param>
    /// <param name="entity">Измененная модель ЮЛ</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Обновленная модель ЮЛ</returns>
    Task<LegalEntity> Update(
        int id,
        LegalEntity entity,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Удалить сведения об ЮЛ
    /// </summary>
    /// <param name="id">Идентификатор ЮЛ</param>
    /// <param name="cancellationToken"></param>
    Task Remove(int id, CancellationToken cancellationToken = default);
}
