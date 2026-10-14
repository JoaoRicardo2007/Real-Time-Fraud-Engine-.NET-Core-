using System;
using MediatR;

namespace FraudEngine.Application.UseCases.EvaluateTransaction
{
    public record EvaluateTransactionCommand
    (
        string UserId,
        decimal Amount,
        string Currency,
        string IpAddress,
        string DeviceFingerprint,
        string Location
    ) : IRequest<EvaluateTransactionResult>;
}
