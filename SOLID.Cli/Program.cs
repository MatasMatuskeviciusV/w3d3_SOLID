using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SOLID.Contracts;
using SOLID.BusinessLogic;
using SOLID.BusinessLogic.Payments;

namespace SOLID.Cli
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            AppSettings.Instance.Environment = "Staging";
            AppSettings.Instance.EnablePaymentLogging = true;
            AppSettings.Instance.EnablePaymentTiming = true;

            ILogger logger = new ConsoleLogger();
            var validator = new OrderValidation();
            INotificationService notifier = new EmailNotification(logger);
            IOrderRepository repository = new OrderRepository(logger);

            IPaymentStrategyFactory factory = new PaymentStrategyFactory(logger);

            var order = new Order
            {
                Id = 1510,
                Total = 200652.27m,
                PaymentMethod = "PayPal",
                CustomerEmail = "customer@example.com"
            };

            var publisher = new OrderEventPublisher();

            publisher.Subscribe(new EmailNotifier(logger));
            publisher.Subscribe(new AuditLogger(logger));

            IOrderService service = new OrderService(validator, notifier, repository, publisher);
            var facade = new OrderFacade(logger, service, factory);

            await facade.PlaceOrder(order);
        }
    }
}