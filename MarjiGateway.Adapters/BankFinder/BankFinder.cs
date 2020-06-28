using System.Threading.Tasks;
using MarjiGateway.Application.Ports;

namespace MarjiGateway.Adapters.BankFinder
{
    public class BankFinder : IBankFinderAdapter
    {
        public Task<string> FindBank(string creditCardNumber)
        {
            return Task.FromResult("hsbc");
        }

        public Task HealthcheckAsync()
        {
            return Task.CompletedTask;
        }
    }
}