using System;
using System.Threading.Tasks;
using MarjiGateway.Application.Ports;
using MarjiGateway.Application.RequestHandlers.ProcessPayment;

namespace MarjiGateway.Adapters.BankAdapters
{
    public class HsbcBankAdapter : IBankAdapter
    {
        public Task<ProcessPaymentResponse> ProcessNewPayment(ProcessPayment payment)
        {
            return Task.FromResult(new ProcessPaymentResponse() { IsSuccess = true, Identifier = Guid.NewGuid().ToString() });
        }

        public Task HealthcheckAsync()
        {
            return Task.CompletedTask;
        }
    }
}
