using FraudEngine.Domain.Enums;

namespace FraudEngine.Application.UseCases.EvaluateTransaction
{
    public record EvaluateTransactionResult
    {
        public TransactionStatus Status { get; init; }
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public int RiskScore { get; init; }
    }
}
