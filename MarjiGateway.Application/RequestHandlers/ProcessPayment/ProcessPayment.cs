using MarjiGateway.Application.Models;
using MediatR;

namespace MarjiGateway.Application.RequestHandlers.ProcessPayment
{
    public class ProcessPayment : IRequest<ProcessPaymentResponse>
    {
        public Payment Payment { get; set; }
    }
}