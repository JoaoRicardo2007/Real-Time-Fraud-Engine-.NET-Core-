using FraudEngine.Application.UseCases.EvaluateTransaction;
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
                request.UserId,
                request.Amount,
                request.Currency,
                request.IpAddress,
                request.DeviceFingerprint,
                request.Location),
            cancellationToken);

        return CreatedAtAction(nameof(Evaluate), new { id = result.Id }, result);
    }
}

public sealed record EvaluateTransactionRequest(
    string UserId,
    decimal Amount,
    string Currency,
    string IpAddress,
    string DeviceFingerprint,
    string Location);