using SOLID.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class AuditLogger : IOrderObserver
    {
        private readonly ILogger _logger;

        public AuditLogger(ILogger logger)
        {
            _logger = logger; 
        }

        public void OnOrderProcessed(Order order)
        {
            _logger.Log($"[AuditLogger] (mock) AUDIT: order {order.Id} processed. Total = {order.Total}, Payment = {order.PaymentMethod}.");
        }
    }
}
