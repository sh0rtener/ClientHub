using ClientHub.UseCases.LegalEntities;
using ClientHub.UseCases.LegalEntities.AddLegalEntity;
using ClientHub.UseCases.LegalEntities.GetAllLegalEntities;
using ClientHub.UseCases.LegalEntities.GetLegalEntity;
using ClientHub.UseCases.LegalEntities.RemoveLegalEntity;
using ClientHub.UseCases.LegalEntities.UpdateLegalEntity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientHub.WebApi.Controllers;

[ApiController]
[Route("api/legal-entities")]
public sealed class LegalEntitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LegalEntitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получение всей информации по ЮЛ
    /// </summary>
    /// <param name="id">Идентификатор ЮЛ</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Информация по ЮЛ</returns>
    /// <response code="200">Успешно!</response>
    /// <response code="400">Ошибка на стороне клиента!</response>
    /// <response code="404">ЮЛ не найдено!</response>
    /// <response code="500">Ошибка сервера!</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<LegalEntityDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<BadRequestApiResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLegalEntityQuery() { Id = id }, cancellationToken);

        if (result is null)
            return this.SendNotFound();
        
        return this.SendOkResult(result);
    }

    /// <summary>
    /// Получение списка ЮЛ 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список ЮЛ с краткой информацией</returns>
    /// <response code="200">Успешно!</response>
    /// <response code="400">Ошибка на стороне клиента!</response>
    /// <response code="500">Ошибка сервера!</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<GetShortLegalEntities>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<BadRequestApiResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServerErrorApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(
        [FromQuery] GetShortLegalEntities request,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(
            new GetAllLegalEntitiesQuery() { Filter = request },
            cancellationToken
        );

        return this.SendOkResult(result);
    }

    /// <summary>
    /// Добавление ЮЛ
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
        [FromBody] LegalEntityDto request,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(
            new AddLegalEntityCommand() { LegalEntity = request },
            cancellationToken
        );

        return this.SendOkResult(result);
    }

    /// <summary>
    /// Обновить сведения об ЮЛ
    /// </summary>
    /// <param name="id">Идентификатор ЮЛ</param>
    /// <param name="request">Обновленные сведения об ЮЛ</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Обновленная модель ЮЛ</returns>
    /// <response code="200">Успешно!</response>
    /// <response code="400">Ошибка на стороне клиента!</response>
    /// <response code="500">Ошибка сервера!</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<LegalEntityDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<BadRequestApiResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServerErrorApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] LegalEntityDto request,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(
            new UpdateLegalEntityCommand() { Id = id, LegalEntity = request },
            cancellationToken
        );

        return this.SendOkResult(result);
    }

    /// <summary>
    /// Удалить сведения об ЮЛ
    /// </summary>
    /// <param name="id">Идентификатор ЮЛ</param>
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
        await _mediator.Send(new RemoveLegalEntityCommand() { Id = id }, cancellationToken);

        return this.SendOkResult();
    }
}
