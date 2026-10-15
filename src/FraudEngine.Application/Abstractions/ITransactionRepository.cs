using FraudEngine.Domain.Entities;

namespace FraudEngine.Application.Abstractions;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
}
