using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Contracts
{
    public interface IPaymentPipeline
    {
        Task ExecuteAsync(decimal amount);
    }
}
