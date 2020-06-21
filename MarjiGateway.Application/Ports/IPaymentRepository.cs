using System.Threading;
using System.Threading.Tasks;
using MarjiGateway.Application.Models;

namespace MarjiGateway.Application.Ports
{
    public interface IPaymentRepository
    {
        Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken);
        Task<Payment> GetAsync(string id, CancellationToken cancellationToken);
    }
}