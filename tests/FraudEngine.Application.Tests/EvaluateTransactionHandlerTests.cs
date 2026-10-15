using FraudEngine.Application.Abstractions;
using FraudEngine.Application.UseCases.EvaluateTransaction;
using FraudEngine.Domain.Entities;
using FraudEngine.Domain.Enums;
using NSubstitute;

namespace FraudEngine.Application.Tests;

public class EvaluateTransactionHandlerTests
{
    [Theory]
    [InlineData(50, TransactionStatus.Approved)]
    [InlineData(80, TransactionStatus.ManualReview)]
    [InlineData(81, TransactionStatus.Rejected)]
    public async Task Handle_should_evaluate_and_persist_transaction(
        int riskScore,
        TransactionStatus expectedStatus)
    {
        var riskScoreEvaluator = Substitute.For<IRiskScoreEvaluator>();
        var transactionRepository = Substitute.For<ITransactionRepository>();
        var cancellationToken = new CancellationTokenSource().Token;
        riskScoreEvaluator.CalculateRiskScore(Arg.Any<Transaction>()).Returns(riskScore);
        var handler = new EvaluateTransactionHandler(riskScoreEvaluator, transactionRepository);

        var result = await handler.Handle(CreateCommand(), cancellationToken);

        Assert.Equal(expectedStatus, result.Status);
        Assert.Equal(riskScore, result.RiskScore);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.NotEqual(default, result.CreatedAt);
        await transactionRepository.Received(1).AddAsync(
            Arg.Is<Transaction>(transaction =>
                transaction.Id == result.Id &&
                transaction.Status == expectedStatus &&
                transaction.RiskScore == riskScore),
            cancellationToken);
    }

    private static EvaluateTransactionCommand CreateCommand()
    {
        return new EvaluateTransactionCommand(
            "user-123",
            100m,
            "EUR",
            "127.0.0.1",
            "device-abc",
            "Lisbon");
    }
}
