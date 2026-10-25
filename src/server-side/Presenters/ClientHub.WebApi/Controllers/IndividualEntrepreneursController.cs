using ClientHub.UseCases.IndividualEntrepreneurs;
using ClientHub.UseCases.IndividualEntrepreneurs.AddIndividualEntrepreneur;
using ClientHub.UseCases.IndividualEntrepreneurs.GetAllIndividualEntrepreneurs;
using ClientHub.UseCases.IndividualEntrepreneurs.GetIndividualEntrepreneur;
using ClientHub.UseCases.IndividualEntrepreneurs.RemoveIndividualEntrepreneur;
using ClientHub.UseCases.IndividualEntrepreneurs.UpdateIndividualEntrepreneur;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientHub.WebApi.Controllers;

[ApiController]
[Route("api/ip")]
public sealed class IndividualEntrepreneursController : ControllerBase
{
    private readonly IMediator _mediator;

    public IndividualEntrepreneursController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получение всей информации по ИП
    /// </summary>
    /// <param name="id">Идентификатор ИП</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Информация по ИП</returns>
    /// <response code="200">Успешно!</response>
    /// <response code="400">Ошибка на стороне клиента!</response>
    /// <response code="404">ИП не найдено!</response>
    /// <response code="500">Ошибка сервера!</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<IndividualEntrepreneursDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<BadRequestApiResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetIndividualEntrepreneurQuery() { Id = id },
            cancellationToken
        );

        if (result is null)
            return this.SendNotFound();

        return this.SendOkResult(result);
    }

    /// <summary>
    /// Получение списка ИП 
    /// </summary>
    /// <param name="request">Фильтр запроса</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список ИП с краткой информацией</returns>
    /// <response code="200">Успешно!</response>
    /// <response code="400">Ошибка на стороне клиента!</response>
    /// <response code="500">Ошибка сервера!</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ShortIndividualEntrepreneursDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<BadRequestApiResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServerErrorApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(
        [FromQuery] GetShortIndividualEntrepreneurs request,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(
            new GetAllIndividualEntrepreneursQuery() { Filter = request },
            cancellationToken
        );

        return this.SendOkResult(result);
    }

    /// <summary>
    /// Добавление ИП
    /// </summary>
    /// <param name="request">Входная модель</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Идентификатор добавленной записи</returns>
    /// <response code="200">Успешно!</response>
    /// <response code="400">Ошибка на стороне клиента!</response>
    /// <response code="500">Ошибка сервера!</response>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<BadRequestApiResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServerErrorApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(
        [FromBody] IndividualEntrepreneursDto request,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(
            new AddIndividualEntrepreneurCommand() { IPs = request },
            cancellationToken
        );

        return this.SendOkResult(result);
    }

    /// <summary>
    /// Обновить сведения об ИП
    /// </summary>
    /// <param name="id">Идентификатор ИП</param>
    /// <param name="request">Обновленные сведения об ИП</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Обновленная модель ИП</returns>
    /// <response code="200">Успешно!</response>
    /// <response code="400">Ошибка на стороне клиента!</response>
    /// <response code="500">Ошибка сервера!</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<IndividualEntrepreneursDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<BadRequestApiResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServerErrorApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] IndividualEntrepreneursDto request,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(
            new UpdateIndividualEntrepreneurCommand() { Id = id, UpdatedModel = request },
            cancellationToken
        );

        return this.SendOkResult(result);
    }

    /// <summary>
    /// Удалить сведения об ИП
    /// </summary>
    /// <param name="id">Идентификатор ИП</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Успешно!</response>
    /// <response code="400">Ошибка на стороне клиента!</response>
    /// <response code="500">Ошибка сервера!</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<BadRequestApiResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServerErrorApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Remove(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new RemoveIndividualEntrepreneurCommand() { Id = id },
            cancellationToken
        );

        return this.SendOkResult();
    }
}
