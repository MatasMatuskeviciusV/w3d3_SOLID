using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLID.Contracts;

namespace SOLID.BusinessLogic
{
    public class GooglePayPaymentProcessing : IPaymentProcessor
    {
        private readonly ILogger _logger;

        public GooglePayPaymentProcessing(ILogger logger)
        {
            _logger = logger;
        }
        public void Process(decimal total)
        {
            _logger.Log($"{total} was charged using Google Pay.");
        }
    }
}
