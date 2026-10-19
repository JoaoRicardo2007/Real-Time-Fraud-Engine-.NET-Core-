using FraudEngine.Application.Abstractions;
using FraudEngine.Domain.Entities;

namespace FraudEngine.Infrastructure.Persistence;

public sealed class TransactionRepository(FraudEngineDbContext dbContext) : ITransactionRepository
{
    public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        await dbContext.Transactions.AddAsync(transaction, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}