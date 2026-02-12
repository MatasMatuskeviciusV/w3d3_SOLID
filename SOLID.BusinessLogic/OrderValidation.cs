using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLID.Contracts;

namespace SOLID.BusinessLogic
{
    public class OrderValidation
    {
        public void ValidateOrder(Order order)
        {
            if (order == null)
            {
                throw new Exception("Order is null");
            }

            if(order.Total <= 0)
            {
                throw new Exception("Total is invalid");
            }
        }
    }
}
