using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MarjiGateway.Application.Models;
using MarjiGateway.Application.Ports;
using Newtonsoft.Json;

namespace MarjiGateway.Adapters.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _path;

        public PaymentRepository()
        {
            var projectBin = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location);
            _path = projectBin + "\\Storage";
            Directory.CreateDirectory(_path);
        }

        public Task<PaymentEntity> CreateAsync(PaymentEntity payment, CancellationToken cancellationToken)
        {
            var json = JsonConvert.SerializeObject(payment);

            var fileName = payment.Identifier + ".json";
            var fullPath = _path + "\\" + fileName;
            File.WriteAllText(fullPath, json);

            return Task.FromResult(payment);
        }

        public Task<PaymentEntity> GetAsync(string id, CancellationToken cancellationToken)
        {
            var fileName = id + ".json";
            var resultStr = File.ReadAllText(_path + fileName);
            var result = JsonConvert.DeserializeObject<PaymentEntity>(resultStr);

            return Task.FromResult(result);
        }
    }
}