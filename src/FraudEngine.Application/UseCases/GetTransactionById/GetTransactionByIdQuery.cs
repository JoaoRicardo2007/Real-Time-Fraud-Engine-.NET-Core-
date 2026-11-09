using MediatR;

namespace FraudEngine.Application.UseCases.GetTransactionById;

public sealed record GetTransactionByIdQuery(Guid Id) : IRequest<TransactionResult?>;

public sealed record TransactionResult(
    Guid Id,
    string UserId,
    decimal Amount,
    string Currency,
    string IpAddress,
    string DeviceFingerprint,
    string Location,
    string Status,
    int RiskScore,
    DateTime CreatedAt);