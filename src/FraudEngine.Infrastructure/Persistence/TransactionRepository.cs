using FraudEngine.Application.Abstractions;
using FraudEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FraudEngine.Infrastructure.Persistence;

public sealed class TransactionRepository(FraudEngineDbContext dbContext) : ITransactionRepository
{
    public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        await dbContext.Transactions.AddAsync(transaction, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<Transaction?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return dbContext.Transactions
            .AsNoTracking()
            .SingleOrDefaultAsync(transaction => transaction.Id == id, cancellationToken);
    }

    public Task<Transaction?> GetByExternalIdAsync(
        string userId,
        string externalTransactionId,
        CancellationToken cancellationToken = default)
    {
        return dbContext.Transactions
            .AsNoTracking()
            .SingleOrDefaultAsync(
                transaction => transaction.UserId == userId
                    && transaction.ExternalTransactionId == externalTransactionId,
                cancellationToken);
    }
}