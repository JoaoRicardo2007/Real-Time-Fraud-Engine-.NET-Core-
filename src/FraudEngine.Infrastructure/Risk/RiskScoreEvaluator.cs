using FraudEngine.Application.Abstractions;
using FraudEngine.Domain.Entities;

namespace FraudEngine.Infrastructure.Risk;

public sealed class RiskScoreEvaluator(IEnumerable<IRiskRule> rules) : IRiskScoreEvaluator
{
    public int CalculateRiskScore(Transaction transaction)
    {
        var score = rules
            .Select(rule => rule.Evaluate(transaction).Score)
            .Sum();

        return Math.Clamp(score, 0, 100);
    }
}