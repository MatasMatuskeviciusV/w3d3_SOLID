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
            IPaymentStrategy paymentStrategy,
            CancellationToken ct = default);
    }
}
