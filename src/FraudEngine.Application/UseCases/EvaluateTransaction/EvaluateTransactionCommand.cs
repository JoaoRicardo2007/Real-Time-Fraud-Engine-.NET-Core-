using System;
using System.Collections.Generic;
using System.Text;

namespace FraudEngine.Application.UseCases.EvaluateTransaction
{
    public record EvaluateTransactionCommand
    (
        string UserId,
        decimal Amount,
        string Currency,
        string IpAddress,
        string DeviceFingerprint,
        string Location
    );
}
