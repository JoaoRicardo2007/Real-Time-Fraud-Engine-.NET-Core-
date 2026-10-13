using FraudEngine.Domain.Entities;

namespace FraudEngine.Application.Abstractions
{
    public interface IRiskScoreEvaluator
    {
        int CalculateRiskScore(Transaction transaction);
    }
}
