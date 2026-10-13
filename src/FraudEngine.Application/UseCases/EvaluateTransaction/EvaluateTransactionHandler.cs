using FraudEngine.Domain.Entities;
using FraudEngine.Application.Abstractions;

namespace FraudEngine.Application.UseCases.EvaluateTransaction;

public class EvaluateTransactionHandler
{
    private readonly IRiskScoreEvaluator _riskScoreEvaluator;

    public EvaluateTransactionHandler(IRiskScoreEvaluator riskScoreEvaluator)
    {
        _riskScoreEvaluator = riskScoreEvaluator;
    }

    public Transaction Handle(EvaluateTransactionCommand command)
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

        return transaction;
    }
}
