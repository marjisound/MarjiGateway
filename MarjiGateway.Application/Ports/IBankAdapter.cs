using System.Threading.Tasks;
using MarjiGateway.Application.RequestHandlers.ProcessPayment;

namespace MarjiGateway.Application.Ports
{
    public interface IBankAdapter
    {
        Task<ProcessPaymentResponse> ProcessNewPayment(ProcessPayment payment);

        Task HealthcheckAsync();
    }
}