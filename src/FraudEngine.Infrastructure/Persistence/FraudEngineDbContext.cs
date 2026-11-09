using FraudEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FraudEngine.Infrastructure.Persistence;

public sealed class FraudEngineDbContext(DbContextOptions<FraudEngineDbContext> options)
    : DbContext(options)
{
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("transactions");
            entity.HasKey(transaction => transaction.Id);

            entity.Property(transaction => transaction.ExternalTransactionId)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(transaction => transaction.UserId).HasMaxLength(100).IsRequired();
            entity.Property(transaction => transaction.Currency).HasMaxLength(3).IsRequired();
            entity.Property(transaction => transaction.IpAddress).HasMaxLength(45).IsRequired();
            entity.Property(transaction => transaction.DeviceFingerprint).HasMaxLength(200).IsRequired();
            entity.Property(transaction => transaction.Location).HasMaxLength(200).IsRequired();
            entity.Property(transaction => transaction.Amount).HasPrecision(18, 2).IsRequired();
            entity.Property(transaction => transaction.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
            entity.Property(transaction => transaction.CreatedAt).IsRequired();

            entity.HasIndex(transaction => transaction.UserId);
            entity.HasIndex(transaction => transaction.CreatedAt);
            entity.HasIndex(transaction => new
            {
                transaction.UserId,
                transaction.ExternalTransactionId
            }).IsUnique();
        });
    }
}