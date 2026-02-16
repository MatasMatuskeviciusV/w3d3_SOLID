using SOLID.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class OrderEventPublisher : IOrderEventPublisher
    {
        private readonly List<IOrderObserver> _observers = new();

        public void Subscribe(IOrderObserver observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }
            _observers.Add(observer);
        }

        public void PublishOrderProcessed(Order order)
        {
            foreach(var observer in _observers)
            {
                observer.OnOrderProcessed(order);
            }
        }
    }
}
