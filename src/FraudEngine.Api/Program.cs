using FluentValidation;
using FraudEngine.Application.Behaviors;
using FraudEngine.Application.Abstractions;
using FraudEngine.Application.UseCases.EvaluateTransaction;
using FraudEngine.Api.ErrorHandling;
using FraudEngine.Infrastructure.Persistence;
using FraudEngine.Infrastructure.Risk;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddValidatorsFromAssemblyContaining<EvaluateTransactionCommandValidator>();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblyContaining<EvaluateTransactionHandler>();
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddDbContext<FraudEngineDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FraudEngine")));
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IRiskRule, AmountRiskRule>();
builder.Services.AddScoped<IRiskScoreEvaluator, RiskScoreEvaluator>();

var app = builder.Build();

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
