using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLID.Contracts;

namespace SOLID.BusinessLogic
{
    public class PaymentLoggingDecorator : IPaymentStrategy
    {
        private readonly IPaymentStrategy _inner;
        private readonly ILogger _logger;

        public PaymentLoggingDecorator(IPaymentStrategy inner, ILogger logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public void Pay(decimal amount)
        {
            if (AppSettings.Instance.EnablePaymentLogging)
            {
                _logger.Log($"[PaymentLogging] Payment starting for {amount}.");
            }
            _inner.Pay(amount);
            if (AppSettings.Instance.EnablePaymentLogging)
            {
                _logger.Log($"[PaymentLogging] Payment finished for {amount}.");
            }
        }
    }
}
