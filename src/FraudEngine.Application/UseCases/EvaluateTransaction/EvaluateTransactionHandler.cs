using FraudEngine.Application.Abstractions;
using FraudEngine.Domain.Entities;
using MediatR;

namespace FraudEngine.Application.UseCases.EvaluateTransaction;

public class EvaluateTransactionHandler : IRequestHandler<EvaluateTransactionCommand, EvaluateTransactionResult>
{
    private readonly IRiskScoreEvaluator _riskScoreEvaluator;

    public EvaluateTransactionHandler(IRiskScoreEvaluator riskScoreEvaluator)
    {
        _riskScoreEvaluator = riskScoreEvaluator;
    }

    public Task<EvaluateTransactionResult> Handle(EvaluateTransactionCommand command, CancellationToken cancellationToken)
    {
        var transaction = new Transaction(
            command.UserId,
            command.Amount,
            command.Currency,
            command.IpAddress,
            command.DeviceFingerprint,
            command.Location);

        var riskScore = _riskScoreEvaluator.CalculateRiskScore(transaction);

        transaction.UpdateTransactionStatus(riskScore);

        return Task.FromResult(new EvaluateTransactionResult
        {
            Id = transaction.Id,
            CreatedAt = transaction.CreatedAt,
            Status = transaction.Status,
            RiskScore = riskScore
        });
    }
}
