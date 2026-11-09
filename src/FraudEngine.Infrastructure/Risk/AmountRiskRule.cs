using FraudEngine.Application.Abstractions;
using FraudEngine.Domain.Entities;

namespace FraudEngine.Infrastructure.Risk;

public sealed class AmountRiskRule : IRiskRule
{
    public RiskRuleResult Evaluate(Transaction transaction)
    {
        var (score, reason) = transaction.Amount switch
        {
            >= 10_000m => (80, "Transaction amount is very high."),
            >= 5_000m => (60, "Transaction amount is high."),
            >= 1_000m => (30, "Transaction amount requires additional attention."),
            _ => (10, "Transaction amount is within the normal initial range.")
        };

        return new RiskRuleResult("amount", score, reason);
    }
}