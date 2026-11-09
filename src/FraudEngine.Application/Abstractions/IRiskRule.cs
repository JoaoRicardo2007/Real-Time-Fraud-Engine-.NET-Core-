using FraudEngine.Domain.Entities;

namespace FraudEngine.Application.Abstractions;

public interface IRiskRule
{
    RiskRuleResult Evaluate(Transaction transaction);
}

public sealed record RiskRuleResult(
    string RuleName,
    int Score,
    string Reason);