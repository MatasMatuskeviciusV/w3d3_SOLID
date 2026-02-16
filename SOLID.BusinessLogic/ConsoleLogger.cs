using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLID.Contracts;

namespace SOLID.BusinessLogic
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            if (!AppSettings.Instance.EnablePaymentLogging)
            {
                return;
            }

            Console.WriteLine($"[{AppSettings.Instance.Environment}] {message}");
        }
    }
}
