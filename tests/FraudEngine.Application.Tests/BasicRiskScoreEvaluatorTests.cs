using FraudEngine.Domain.Entities;
using FraudEngine.Infrastructure.Risk;

namespace FraudEngine.Application.Tests;

public class BasicRiskScoreEvaluatorTests
{
    [Theory]
    [InlineData(100, 10)]
    [InlineData(1_000, 30)]
    [InlineData(5_000, 60)]
    [InlineData(10_000, 80)]
    public void CalculateRiskScore_should_apply_amount_bands(decimal amount, int expectedScore)
    {
        var transaction = new Transaction(
            "external-123",
            "user-123",
            amount,
            "EUR",
            "127.0.0.1",
            "device-abc",
            "Lisbon");

        var evaluator = new BasicRiskScoreEvaluator();

        var score = evaluator.CalculateRiskScore(transaction);

        Assert.Equal(expectedScore, score);
    }
}