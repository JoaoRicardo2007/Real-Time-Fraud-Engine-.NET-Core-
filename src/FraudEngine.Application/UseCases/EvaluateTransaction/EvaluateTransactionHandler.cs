using FraudEngine.Application.Abstractions;
using FraudEngine.Domain.Entities;
using MediatR;

namespace FraudEngine.Application.UseCases.EvaluateTransaction;

public class EvaluateTransactionHandler : IRequestHandler<EvaluateTransactionCommand, EvaluateTransactionResult>
{
    private readonly IRiskScoreEvaluator _riskScoreEvaluator;
    private readonly ITransactionRepository _transactionRepository;

    public EvaluateTransactionHandler(
        IRiskScoreEvaluator riskScoreEvaluator,
        ITransactionRepository transactionRepository)
    {
        _riskScoreEvaluator = riskScoreEvaluator;
        _transactionRepository = transactionRepository;
    }

    public async Task<EvaluateTransactionResult> Handle(
        EvaluateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var transaction = new Transaction(
            command.UserId,
            command.Amount,
            command.Currency,
            command.IpAddress,
            command.DeviceFingerprint,
            command.Location);

        var riskScore = _riskScoreEvaluator.CalculateRiskScore(transaction);

        transaction.UpdateTransactionStatus(riskScore);

        await _transactionRepository.AddAsync(transaction, cancellationToken);

        return new EvaluateTransactionResult
        {
            Id = transaction.Id,
            CreatedAt = transaction.CreatedAt,
            Status = transaction.Status,
            RiskScore = riskScore
        };
    }
}
