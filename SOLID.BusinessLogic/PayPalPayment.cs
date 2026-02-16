using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLID.Contracts;

namespace SOLID.BusinessLogic
{
    public class PayPalPayment : IPaymentStrategy
    {
        private readonly ILogger _logger;

        public PayPalPayment(ILogger logger)
        {
            _logger = logger;
        }

        public void Pay(decimal total)
        {
            _logger.Log($"{total} was paid using PayPal (mock).");
        }

    }
}
