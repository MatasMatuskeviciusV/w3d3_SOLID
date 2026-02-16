using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Contracts
{
    public interface IOrderObserver
    {
        void OnOrderProcessed(Order order);
    }
}
