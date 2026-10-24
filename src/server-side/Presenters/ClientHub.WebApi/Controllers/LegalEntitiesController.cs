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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLegalEntityQuery() { Id = id }, cancellationToken);
        return this.SendOkResult(result);
    }

    [HttpGet("all")]
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

    [HttpPost]
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

    [HttpPut("{id:int}")]
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

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RemoveLegalEntityCommand() { Id = id }, cancellationToken);

        return this.SendOkResult();
    }
}
