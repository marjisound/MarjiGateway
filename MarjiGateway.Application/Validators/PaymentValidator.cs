﻿using FluentValidation;
using MarjiGateway.Application.Models;
using MarjiGateway.Application.Validators.Common;

namespace MarjiGateway.Application.Validators
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(payment => payment.CardNumber).NotEmpty()
                .WithMessage("Card number is required").WithErrorCode(ErrorCodes.CardNumberIsRequired);
            RuleFor(payment => payment.Amount).Custom((x, context )=>
            {
                
            });
            RuleFor(payment => payment.Cvv).NotEmpty().Length(3);
            RuleFor(payment => payment.ExpiryDay).InclusiveBetween(1, 31);
            RuleFor(payment => payment.ExpiryYear).InclusiveBetween(1, 12);
        }
    }
}