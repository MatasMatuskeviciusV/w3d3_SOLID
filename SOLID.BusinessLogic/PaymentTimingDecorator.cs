using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLID.Contracts;

namespace SOLID.BusinessLogic
{
    public class PaymentTimingDecorator : IPaymentStrategy
    {
        private readonly IPaymentStrategy _inner;
        private readonly ILogger _logger;

        public PaymentTimingDecorator(IPaymentStrategy inner, ILogger logger)
        {
            _inner = inner; 
            _logger = logger;
        }

        public void Pay(decimal amount)
        {
            if (!AppSettings.Instance.EnablePaymentTiming)
            {
                _inner.Pay(amount);
                return;
            }

            var sw = Stopwatch.StartNew();
            _inner.Pay(amount);
            sw.Stop();

            _logger.Log($"[PaymentTiming] {_inner.GetType().Name} took {sw.ElapsedMilliseconds} ms.");
        }
    }
}
