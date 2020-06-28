using MediatR;

namespace MarjiGateway.Application.RequestHandlers.GetPayment
{
    public class GetPaymentQuery : IRequest<GetPaymentResponse>
    {
        public string Identifier { get; set; }
    }
}