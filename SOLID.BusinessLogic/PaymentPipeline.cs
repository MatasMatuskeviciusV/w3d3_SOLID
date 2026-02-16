using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLID.Contracts;

namespace SOLID.BusinessLogic
{
    public class PaymentPipeline : IPaymentPipeline
    {
        private readonly IReadOnlyList<IPaymentStep> _steps;

        public PaymentPipeline(IReadOnlyList<IPaymentStep> steps)
        {
            _steps = steps ?? throw new ArgumentNullException(nameof(steps));
        }

        public Task ExecuteAsync(decimal amount)
        {
            Func<Task> next = () => Task.CompletedTask;

            for(int i = _steps.Count - 1; i >= 0; i--)
            {
                var step = _steps[i];
                var currentNext = next;
                next = () => step.Handle(amount, currentNext);
            }

            return next();
        }
    }
}
