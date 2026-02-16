using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLID.Contracts;

namespace SOLID.BusinessLogic
{
    public class PaymentValidationStep : IPaymentStep
    {
        public async Task Handle(decimal amount, Func<Task> next)
        {
            if(amount <= 0)
            {
                throw new InvalidOperationException("Payment amount must be greater than 0.");
            }
            await next();
        }
    }
}
