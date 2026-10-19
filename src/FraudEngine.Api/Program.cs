using FraudEngine.Application.Abstractions;
using FraudEngine.Application.UseCases.EvaluateTransaction;
using FraudEngine.Infrastructure.Persistence;
using FraudEngine.Infrastructure.Risk;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMediatR(configuration =>
    configuration.RegisterServicesFromAssemblyContaining<EvaluateTransactionHandler>());
builder.Services.AddDbContext<FraudEngineDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FraudEngine")));
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IRiskScoreEvaluator, BasicRiskScoreEvaluator>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
