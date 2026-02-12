using SOLID.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class EmailNotification : INotificationService
    {
        private readonly ILogger _logger;

        public EmailNotification(ILogger logger)
        {
            _logger = logger; 
        }

        public void Send(string recipient, string message)
        {
            _logger.Log($"Order confirmation '{message}' was sent to {recipient}");
        }
    }
}
