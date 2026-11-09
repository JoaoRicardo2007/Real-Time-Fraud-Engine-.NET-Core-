using FluentValidation;
using System.Net;

namespace FraudEngine.Application.UseCases.EvaluateTransaction;

public sealed class EvaluateTransactionCommandValidator
    : AbstractValidator<EvaluateTransactionCommand>
{
    public EvaluateTransactionCommandValidator()
    {
        RuleFor(command => command.ExternalTransactionId)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.UserId)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.Amount)
            .GreaterThan(0);

        RuleFor(command => command.Currency)
            .NotEmpty()
            .Length(3)
            .Matches("^[A-Za-z]{3}$");

        RuleFor(command => command.IpAddress)
            .NotEmpty()
            .Must(ipAddress => IPAddress.TryParse(ipAddress, out _))
            .WithMessage("A valid IP address is required.");

        RuleFor(command => command.DeviceFingerprint)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(command => command.Location)
            .NotEmpty()
            .MaximumLength(200);
    }
}