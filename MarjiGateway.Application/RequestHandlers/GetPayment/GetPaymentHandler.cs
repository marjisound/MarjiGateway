using System.Threading;
using System.Threading.Tasks;
using MarjiGateway.Application.Ports;
using MediatR;

namespace MarjiGateway.Application.RequestHandlers.GetPayment
{
    public class GetPaymentHandler : IRequestHandler<GetPaymentQuery, GetPaymentResponse>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<GetPaymentResponse> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetAsync(request.Identifier, cancellationToken);

            return new GetPaymentResponse() { Payment = payment };
        }
    }
}