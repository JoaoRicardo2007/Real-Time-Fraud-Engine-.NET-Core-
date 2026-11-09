using FraudEngine.Domain.Entities;

namespace FraudEngine.Application.Abstractions;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);

    Task<Transaction?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Transaction?> GetByExternalIdAsync(
        string userId,
        string externalTransactionId,
        CancellationToken cancellationToken = default);
}
