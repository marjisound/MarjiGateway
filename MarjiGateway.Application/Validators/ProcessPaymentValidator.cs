using FluentValidation;
using MarjiGateway.Application.Models;
using MarjiGateway.Application.RequestHandlers.ProcessPayment;

namespace MarjiGateway.Application.Validators
{
    public class ProcessPaymentValidator : AbstractValidator<ProcessPayment>
    {
        private readonly IValidator<Payment> _paymentValidator;

        public ProcessPaymentValidator(IValidator<Payment> paymentValidator)
        {
            _paymentValidator = paymentValidator;

            RuleFor(processPayment => processPayment.Payment)
                .NotNull()
                .SetValidator(_paymentValidator);
        }
    }
}