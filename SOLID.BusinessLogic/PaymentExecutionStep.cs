using SOLID.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class PaymentExecutionStep : IPaymentStep
    {
        private readonly IPaymentStrategy _strategy;

        public PaymentExecutionStep(IPaymentStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public async Task Handle(decimal amount, Func<Task> next)
        {
            _strategy.Pay(amount);
            await next();
        }
    }
}
