using SOLID.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class PaymentAuditStep : IPaymentStep
    {
        private readonly ILogger _logger;

        public PaymentAuditStep(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Handle(decimal amount, Func<Task> next)
        {
            _logger.Log($"[PaymentAudit] Starting payment for {amount}.");
            await next();
            _logger.Log($"[PaymentAudit] Finished payment for {amount}.");
        }
    }
}
