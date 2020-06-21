using System.Threading;
using System.Threading.Tasks;
using MarjiGateway.Application.Ports;
using MarjiGateway.Application.Providers;
using MediatR;

namespace MarjiGateway.Application.RequestHandlers.ProcessPayment
{
    public class ProcessPaymentHandler : IRequestHandler<ProcessPayment, ProcessPaymentResponse>
    {
        private readonly IBankProviderFactory _bankProviderFactory;
        private readonly IBankFinderAdapter _bankFinderAdapter;

        public ProcessPaymentHandler(IBankProviderFactory bankProviderFactory, IBankFinderAdapter bankFinderAdapter)
        {
            _bankProviderFactory = bankProviderFactory;
            _bankFinderAdapter = bankFinderAdapter;
        }
        public async Task<ProcessPaymentResponse> Handle(ProcessPayment request, CancellationToken cancellationToken)
        {
            var bankName = await _bankFinderAdapter.FindBank(request.Payment.CardNumber);
            var bank = _bankProviderFactory.Create(bankName);
            return await bank.ProcessNewPayment(request);
        }
    }
}