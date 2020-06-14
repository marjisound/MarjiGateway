using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MarjiGateway.Application.RequestHandlers.ProcessPayment
{
    public class ProcessPaymentHandler : IRequestHandler<ProcessPayment, ProcessPaymentResponse>
    {
        public Task<ProcessPaymentResponse> Handle(ProcessPayment request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ProcessPaymentResponse() {IsSuccess = true});
        }
    }
}