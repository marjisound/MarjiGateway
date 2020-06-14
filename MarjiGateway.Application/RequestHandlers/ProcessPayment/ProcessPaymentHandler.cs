using System.Threading;
using System.Threading.Tasks;

namespace MarjiGateway.Application.RequestHandlers.ProcessPayment
{
    public class ProcessPaymentHandler
    {
        public Task Handle(ProcessPayment processPayment, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}