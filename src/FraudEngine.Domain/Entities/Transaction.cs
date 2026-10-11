using System;
using FraudEngine.Domain.Enums;

namespace FraudEngine.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }
    public string UserId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    public string IpAddress { get; private set; }
    public string DeviceFingerprint { get; private set; }
    public string Location { get; private set; }
    public TransactionStatus Status { get; private set; }
    public int RiskScore { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Construtor para criar uma nova transação pendente
    public Transaction(string userId, decimal amount, string currency, string ipAddress, string deviceFingerprint, string location)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);
        ArgumentException.ThrowIfNullOrWhiteSpace(currency);
        ArgumentException.ThrowIfNullOrWhiteSpace(ipAddress);
        ArgumentException.ThrowIfNullOrWhiteSpace(deviceFingerprint);
        ArgumentException.ThrowIfNullOrWhiteSpace(location);

        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Transaction amount must be greater than zero");
        }

        Id = Guid.NewGuid();
        UserId = userId;
        Amount = amount;
        Currency = currency;
        IpAddress = ipAddress;
        DeviceFingerprint = deviceFingerprint;
        Location = location;
        Status = TransactionStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    // Método para atualizar o status da transação
    public void UpdateTransactionStatus(int riskScore)
    {
        if (Status != TransactionStatus.Pending)
        {
            throw new InvalidOperationException(
                "Only pending transactions can be evaluated.");
        }

        if (riskScore is < 0 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(riskScore), "Risk score must be between 0 and 100.");
        }

        RiskScore = riskScore;

        Status = riskScore switch
        {
            <= 50 => TransactionStatus.Approved,
            <= 80 => TransactionStatus.ManualReview,
            _ => TransactionStatus.Rejected
        };
    }
}