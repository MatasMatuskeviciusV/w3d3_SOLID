using SOLID.BusinessLogic.Payments;
using SOLID.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class PaymentStrategyFactory : IPaymentStrategyFactory
    {
        private readonly Dictionary<string, Func<IPaymentStrategy>> _map;

        // cia pasiklaust
        public PaymentStrategyFactory(ILogger logger)
        {
            _map = new Dictionary<string, Func<IPaymentStrategy>>(StringComparer.OrdinalIgnoreCase)
            {
                ["CreditCard"] = () => new CreditCardPayment(logger),
                ["PayPal"] = () => new PayPalPayment(logger),
                ["ApplePay"] = () => new ApplePayPayment(logger),
                ["GooglePay"] = () => new GooglePayPayment(logger),
                ["BankTransfer"] = () => new BankTransferPayment(logger)
            };
        }

        public IPaymentStrategy Create(string paymentMethod)
        {
            if (!_map.TryGetValue(paymentMethod, out var constructor))
            {
                throw new InvalidOperationException($"Unknown payment method: {paymentMethod}");
            }
            return constructor();
        }

    }
}
