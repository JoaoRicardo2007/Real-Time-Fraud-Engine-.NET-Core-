using FraudEngine.Application.UseCases.EvaluateTransaction;

namespace FraudEngine.Application.Tests;

public class EvaluateTransactionCommandValidatorTests
{
    private readonly EvaluateTransactionCommandValidator _validator = new();

    [Fact]
    public async Task Validate_should_reject_invalid_amount_and_ip()
    {
        var command = new EvaluateTransactionCommand(
            "external-123",
            "user-123",
            0,
            "EUR",
            "invalid-ip",
            "device-abc",
            "Lisbon");

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(command.Amount));
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(command.IpAddress));
    }

    [Fact]
    public async Task Validate_should_accept_valid_command()
    {
        var command = new EvaluateTransactionCommand(
            "external-123",
            "user-123",
            100,
            "EUR",
            "127.0.0.1",
            "device-abc",
            "Lisbon");

        var result = await _validator.ValidateAsync(command);

        Assert.True(result.IsValid);
    }
}