using FraudEngine.Application.Abstractions;
using FraudEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FraudEngine.Application.UseCases.EvaluateTransaction;

public class EvaluateTransactionHandler : IRequestHandler<EvaluateTransactionCommand, EvaluateTransactionResult>
{
    private readonly IRiskScoreEvaluator _riskScoreEvaluator;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ILogger<EvaluateTransactionHandler> _logger;

    public EvaluateTransactionHandler(
        IRiskScoreEvaluator riskScoreEvaluator,
        ITransactionRepository transactionRepository,
        ILogger<EvaluateTransactionHandler> logger)
    {
        _riskScoreEvaluator = riskScoreEvaluator;
        _transactionRepository = transactionRepository;
        _logger = logger;
    }

    public async Task<EvaluateTransactionResult> Handle(
        EvaluateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var existingTransaction = await _transactionRepository.GetByExternalIdAsync(
            command.UserId,
            command.ExternalTransactionId,
            cancellationToken);

        if (existingTransaction is not null)
        {
            return ToResult(existingTransaction);
        }

        var transaction = new Transaction(
            command.ExternalTransactionId,
            command.UserId,
            command.Amount,
            command.Currency,
            command.IpAddress,
            command.DeviceFingerprint,
            command.Location);

        var riskScore = _riskScoreEvaluator.CalculateRiskScore(transaction);

        transaction.UpdateTransactionStatus(riskScore);

        await _transactionRepository.AddAsync(transaction, cancellationToken);

        _logger.LogInformation(
            "Transaction {TransactionId} evaluated with score {RiskScore} and status {Status}",
            transaction.Id,
            riskScore,
            transaction.Status);

        return ToResult(transaction);
    }

    private static EvaluateTransactionResult ToResult(Transaction transaction)
    {
        return new EvaluateTransactionResult
        {
            Id = transaction.Id,
            CreatedAt = transaction.CreatedAt,
            Status = transaction.Status,
            RiskScore = transaction.RiskScore
        };
    }
}
