using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLID.Contracts;

namespace SOLID.BusinessLogic
{
    public class EmailNotifier : IOrderObserver
    {
        private readonly ILogger _logger;

        public EmailNotifier(ILogger logger)
        {
            _logger = logger; 
        }

        public void OnOrderProcessed(Order order)
        {
            _logger.Log($"[EmailNotifier] (mock) Email sent to {order.CustomerEmail ?? "(no email)"} for order {order.Id}.");
        }
    }
}
