using System;
using System.Threading;
using System.Threading.Tasks;
using MarjiGateway.Application.Models;
using MarjiGateway.Application.Ports;
using MarjiGateway.Application.Providers;
using MediatR;

namespace MarjiGateway.Application.RequestHandlers.ProcessPayment
{
    public class ProcessPaymentHandler : IRequestHandler<ProcessPayment, ProcessPaymentResponse>
    {
        private readonly IBankProviderFactory _bankProviderFactory;
        private readonly IBankFinderAdapter _bankFinderAdapter;
        private readonly IPaymentRepository _paymentRepository;

        public ProcessPaymentHandler(IBankProviderFactory bankProviderFactory, IBankFinderAdapter bankFinderAdapter, IPaymentRepository paymentRepository)
        {
            _bankProviderFactory = bankProviderFactory;
            _bankFinderAdapter = bankFinderAdapter;
            _paymentRepository = paymentRepository;
        }
        public async Task<ProcessPaymentResponse> Handle(ProcessPayment request, CancellationToken cancellationToken)
        {
            var bankName = await _bankFinderAdapter.FindBank(request.Payment.CardNumber);
            var bank = _bankProviderFactory.Create(bankName);
            var paymentResponse = await bank.ProcessNewPayment(request);

            var paymentEntity = await _paymentRepository.CreateAsync(CreatePaymentEntity(request.Payment, paymentResponse), cancellationToken);
            return paymentResponse;
        }

        private PaymentEntity CreatePaymentEntity(Payment request, ProcessPaymentResponse response)
        {
            return new PaymentEntity()
            {
                Amount = request.Amount,
                CardNumber = request.CardNumber,
                Currency = request.Currency,
                Cvv = request.Cvv,
                ExpiryMonth = request.ExpiryMonth,
                ExpiryYear = request.ExpiryYear,
                Created = DateTime.Now,
                Identifier = response.Identifier,
                Status = PaymentStatus.Completed
            };
        }
    }
}