using System.Threading.Tasks;
using MarjiGateway.Application.Ports;
using Moq;

namespace MarjiGateway.SpecificationTests.Facads
{
    public class BankFinderFacade
    {
        private readonly Mock<IBankFinderAdapter> _bankFinder;

        public BankFinderFacade(Mock<IBankFinderAdapter> bankFinder)
        {
            _bankFinder = bankFinder;
        }

        public void ConfigureFindBank(string bank)
        {
            _bankFinder.Setup(x => x.FindBank(It.IsAny<string>()))
                .Returns(() => Task.FromResult(bank));
        }
    }
}