using System.Threading;
using System.Threading.Tasks;
using MarjiGateway.Application.Models;

namespace MarjiGateway.Application.Ports
{
    public interface IPaymentRepository
    {
        Task<PaymentEntity> CreateAsync(PaymentEntity payment, CancellationToken cancellationToken);
        Task<PaymentEntity> GetAsync(string id, CancellationToken cancellationToken);
    }
}