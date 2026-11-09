using FraudEngine.Application.UseCases.EvaluateTransaction;
using FraudEngine.Application.UseCases.GetTransactionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FraudEngine.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public sealed class TransactionsController(ISender sender) : ControllerBase
{
    [HttpPost("evaluate")]
    [ProducesResponseType(typeof(EvaluateTransactionResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EvaluateTransactionResult>> Evaluate(
        [FromBody] EvaluateTransactionRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new EvaluateTransactionCommand(
                request.ExternalTransactionId,
                request.UserId,
                request.Amount,
                request.Currency,
                request.IpAddress,
                request.DeviceFingerprint,
                request.Location),
            cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TransactionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransactionResult>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetTransactionByIdQuery(id),
            cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }
}

public sealed record EvaluateTransactionRequest(
    string ExternalTransactionId,
    string UserId,
    decimal Amount,
    string Currency,
    string IpAddress,
    string DeviceFingerprint,
    string Location);