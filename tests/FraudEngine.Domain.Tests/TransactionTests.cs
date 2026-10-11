using FraudEngine.Domain.Entities;
using FraudEngine.Domain.Enums;

namespace FraudEngine.Domain.Tests;

public class TransactionTests
{
    [Fact]
    public void New_transaction_should_start_as_pending()
    {
        var transaction = new Transaction(
            "user-123",
            100m,
            "EUR",
            "127.0.0.1",
            "device-abc",
            "Lisbon");

        Assert.Equal(TransactionStatus.Pending, transaction.Status);
        Assert.Equal(0, transaction.RiskScore);
    }
    [Theory]
    [InlineData(50, TransactionStatus.Approved)]
    [InlineData(51, TransactionStatus.ManualReview)]
    [InlineData(80, TransactionStatus.ManualReview)]
    [InlineData(81, TransactionStatus.Rejected)]
    public void Update_transaction_status_should_classify_risk(
    int riskScore,
    TransactionStatus expectedStatus)
    {
        var transaction = new Transaction(
            "user-123",
            100m,
            "EUR",
            "127.0.0.1",
            "device-abc",
            "Lisbon");

        transaction.UpdateTransactionStatus(riskScore);

        Assert.Equal(expectedStatus, transaction.Status);
        Assert.Equal(riskScore, transaction.RiskScore);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void Update_transaction_status_should_reject_invalid_risk_score(
        int riskScore)
    {
        var transaction = new Transaction(
            "user-123",
            100m,
            "EUR",
            "127.0.0.1",
            "device-abc",
            "Lisbon");

        Assert.Throws<ArgumentOutOfRangeException>(
            () => transaction.UpdateTransactionStatus(riskScore));
    }

    [Fact]
    public void Update_transaction_status_should_not_allow_re_evaluation()
    {
        var transaction = new Transaction(
            "user-123",
            100m,
            "EUR",
            "127.0.0.1",
            "device-abc",
            "Lisbon");

        transaction.UpdateTransactionStatus(50);

        Assert.Throws<InvalidOperationException>(
            () => transaction.UpdateTransactionStatus(90));
    }
}