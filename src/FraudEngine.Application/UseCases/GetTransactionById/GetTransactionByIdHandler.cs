using FraudEngine.Application.Abstractions;
using MediatR;

namespace FraudEngine.Application.UseCases.GetTransactionById;

public sealed class GetTransactionByIdHandler(ITransactionRepository repository)
    : IRequestHandler<GetTransactionByIdQuery, TransactionResult?>
{
    public async Task<TransactionResult?> Handle(
        GetTransactionByIdQuery query,
        CancellationToken cancellationToken)
    {
        var transaction = await repository.GetByIdAsync(query.Id, cancellationToken);

        return transaction is null
            ? null
            : new TransactionResult(
                transaction.Id,
                transaction.UserId,
                transaction.Amount,
                transaction.Currency,
                transaction.IpAddress,
                transaction.DeviceFingerprint,
                transaction.Location,
                transaction.Status.ToString(),
                transaction.RiskScore,
                transaction.CreatedAt);
    }
}