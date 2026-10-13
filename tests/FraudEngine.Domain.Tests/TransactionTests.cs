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

    [Fact]
    public void New_transaction_should_reject_empty_user_id()
    {
        Assert.Throws<ArgumentException>(() => new Transaction(
            "",
            100m,
            "EUR",
            "127.0.0.1",
            "device-abc",
            "Lisbon"));
    }

    [Fact]
    public void New_transaction_should_reject_empty_currency()
    {
        Assert.Throws<ArgumentException>(() => new Transaction(
            "user-123",
            100m,
            "",
            "127.0.0.1",
            "device-abc",
            "Lisbon"));
    }

    [Fact]
    public void New_transaction_should_reject_empty_ip_address()
    {
        Assert.Throws<ArgumentException>(() => new Transaction(
            "user-123",
            100m,
            "EUR",
            "",
            "device-abc",
            "Lisbon"));
    }

    [Fact]
    public void New_transaction_should_reject_empty_device_fingerprint()
    {
        Assert.Throws<ArgumentException>(() => new Transaction(
            "user-123",
            100m,
            "EUR",
            "127.0.0.1",
            "",
            "Lisbon"));
    }

    [Fact]
    public void New_transaction_should_reject_empty_location()
    {
        Assert.Throws<ArgumentException>(() => new Transaction(
            "user-123",
            100m,
            "EUR",
            "127.0.0.1",
            "device-abc",
            ""));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void New_transaction_should_reject_non_positive_amount(decimal amount)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Transaction(
            "user-123",
            amount,
            "EUR",
            "127.0.0.1",
            "device-abc",
            "Lisbon"));
    }

    [Theory]
    [InlineData(0, TransactionStatus.Approved)]
    [InlineData(50, TransactionStatus.Approved)]
    [InlineData(51, TransactionStatus.ManualReview)]
    [InlineData(80, TransactionStatus.ManualReview)]
    [InlineData(81, TransactionStatus.Rejected)]
    [InlineData(100, TransactionStatus.Rejected)]
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

        Assert.Equal(TransactionStatus.Pending, transaction.Status);
        Assert.Equal(0, transaction.RiskScore);
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

        Assert.Equal(TransactionStatus.Approved, transaction.Status);
        Assert.Equal(50, transaction.RiskScore);
    }
}