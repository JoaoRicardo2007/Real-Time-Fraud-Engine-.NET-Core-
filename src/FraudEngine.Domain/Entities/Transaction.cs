using System;
using System.Collections.Generic;
using System.Text;

namespace FraudEngine.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public string UserId { get; private set; }
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }
        public string IpAddress { get; private set; }
        public string DeviceFingerprint { get; private set; }
        public string Location { get; private set; }
        public string Status { get; private set; } // Pending, Approved, Rejected
        public int RiskScore { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Construtor para criar uma nova transação pendente
        public Transaction(string userId, decimal amount, string currency, string ipAddress, string deviceFingerprint, string location)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Amount = amount;
            Currency = currency;
            IpAddress = ipAddress;
            DeviceFingerprint = deviceFingerprint;
            Location = location;
            Status = "Pending";
            RiskScore = 0;
            CreatedAt = DateTime.UtcNow;
        }

        // Método para atualizar o score de risco após passar pelo Motor de Fraude
        public void UpdateRiskScore(int score)
        {
            RiskScore = score;
            Status = score > 50 ? "Rejected" : "Approved";
        }
    }
}
