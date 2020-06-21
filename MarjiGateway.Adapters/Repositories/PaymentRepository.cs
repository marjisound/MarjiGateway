using System.Threading;
using System.Threading.Tasks;
using MarjiGateway.Application.Models;
using MarjiGateway.Application.Ports;
using Newtonsoft.Json;

namespace MarjiGateway.Adapters.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken)
        {
            var json = JsonConvert.SerializeObject(payment);
            //var fileName = payment.

            throw new System.NotImplementedException();
        }

        public Task<Payment> GetAsync(string id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}