using ClientHub.Domain.Clients.CourtValues;

namespace ClientHub.Domain.IndividualEntrepreneurs;

public interface IIndividualEntrepreneurRepository
{
    /// <summary>
    /// Получение полной информации об ИП
    /// </summary>
    /// <param name="id">Идентификатор ИП</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Полная инфомрация о ИП, если существует, иначе null</returns>
    Task<IndividualEntrepreneur?> Get(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверяет, существует ли ИП по переданному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор ИП</param>
    /// <param name="cancellationToken"></param>
    /// <returns>True, если существует, иначе False</returns>
    Task<bool> IsExist(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверяет, существует ли ИП по переданному ИНН
    /// </summary>
    /// <param name="inn">ИНН</param>
    /// <param name="cancellationToken"></param>
    /// <returns>True, если существует, иначе False</returns>
    Task<bool> IsExist(Inn inn, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавление ИП
    /// </summary>
    /// <param name="entity">ИП</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Идентификатор добавленной записи</returns>
    Task<int> Add(IndividualEntrepreneur entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить сведения об ИП
    /// </summary>
    /// <param name="id">Идентификатор ИП</param>
    /// <param name="entity">Измененная модель</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Обновленная модель</returns>
    Task<IndividualEntrepreneur> Update(
        int id,
        IndividualEntrepreneur entity,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Удалить ИП
    /// </summary>
    /// <param name="id">Идентификатор ИП</param>
    /// <param name="cancellationToken"></param>
    Task Remove(int id, CancellationToken cancellationToken = default);
}
