using ClientHub.UseCases.IndividualEntrepreneurs;
using ClientHub.UseCases.IndividualEntrepreneurs.AddIndividualEntrepreneur;
using ClientHub.UseCases.IndividualEntrepreneurs.GetAllIndividualEntrepreneurs;
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

    [HttpGet("all")]
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

    [HttpPost]
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

    [HttpPut("{id:int}")]
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

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new RemoveIndividualEntrepreneurCommand() { Id = id },
            cancellationToken
        );

        return this.SendOkResult();
    }
}
