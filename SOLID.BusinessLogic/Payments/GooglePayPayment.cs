using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLID.Contracts;

namespace SOLID.BusinessLogic.Payments
{
    public class GooglePayPayment : IPaymentStrategy
    {
        private readonly ILogger _logger;

        public GooglePayPayment(ILogger logger)
        {
            _logger = logger;
        }

        public void Pay(decimal total)
        {
            _logger.Log($"{total} was paid using Google Pay (mock).");
        }

    }
}
