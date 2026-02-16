using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Contracts
{
    public interface IOrderService
    {
        Task ProcessOrderAsync(
            Order order,
            IPaymentPipeline paymentPipeline,
            CancellationToken ct = default);
    }
}
