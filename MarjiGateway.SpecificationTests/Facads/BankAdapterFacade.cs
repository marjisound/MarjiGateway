using System.Threading.Tasks;
using MarjiGateway.Application.Ports;
using MarjiGateway.Application.RequestHandlers.ProcessPayment;
using Moq;

namespace MarjiGateway.SpecificationTests.Facads
{
    public class BankAdapterFacade
    {
        private readonly Mock<IBankAdapter> _hsbsBankAdapter;

        public BankAdapterFacade(Mock<IBankAdapter> hsbsBankAdapter)
        {
            _hsbsBankAdapter = hsbsBankAdapter;
        }

        public int NumberOfCallsToProcessNewPayment { get; set; }

        public void ConfigureProcessNewPaymentAsync(ProcessPaymentResponse paymentResponse)
        {
            _hsbsBankAdapter.Setup(x => x.ProcessNewPayment(It.IsAny<ProcessPayment>()))
                .Returns(() =>
                {
                    NumberOfCallsToProcessNewPayment++;
                    return Task.FromResult(paymentResponse);
                });
        }
    }
}