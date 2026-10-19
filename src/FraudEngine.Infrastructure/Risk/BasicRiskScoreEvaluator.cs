using FraudEngine.Application.Abstractions;
using FraudEngine.Domain.Entities;

namespace FraudEngine.Infrastructure.Risk;

public sealed class BasicRiskScoreEvaluator : IRiskScoreEvaluator
{
    public int CalculateRiskScore(Transaction transaction)
    {
        var score = transaction.Amount switch
        {
            >= 10_000m => 80,
            >= 5_000m => 60,
            >= 1_000m => 30,
            _ => 10
        };

        return Math.Clamp(score, 0, 100);
    }
}