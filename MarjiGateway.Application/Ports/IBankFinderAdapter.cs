using System.Threading.Tasks;

namespace MarjiGateway.Application.Ports
{
    public interface IBankFinderAdapter
    {
        Task<string> FindBank(string creditCardNumber);
        Task HealthcheckAsync();
    }
}