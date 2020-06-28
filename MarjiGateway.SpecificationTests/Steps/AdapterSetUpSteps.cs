using MarjiGateway.Application.RequestHandlers.ProcessPayment;
using MarjiGateway.SpecificationTests.Facads;

namespace MarjiGateway.SpecificationTests.Steps
{
    public class AdapterSetUpSteps
    {
        private readonly BankAdapterFacade _bankAdapterFacade;
        private readonly BankFinderFacade _bankFinderFacade;

        public AdapterSetUpSteps(BankAdapterFacade bankAdapterFacade, BankFinderFacade bankFinderFacade)
        {
            _bankAdapterFacade = bankAdapterFacade;
            _bankFinderFacade = bankFinderFacade;
        }

        public void BankAdapterReturnsProcessedPayment(ProcessPaymentResponse paymentResponse)
        {
            _bankAdapterFacade.ConfigureProcessNewPaymentAsync(paymentResponse);
        }

        public void BankFinderReturns(string bankName)
        {
            _bankFinderFacade.ConfigureFindBank(bankName);
        }
    }
}