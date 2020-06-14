using System;
using System.ComponentModel.DataAnnotations;

namespace MarjiGateway.Application.Models
{
    public class Payment
    {
        [CreditCard]
        public string CardNumber { get; set; }

        [Range(2012, 2100)]
        public int ExpiryYear { get; set; }

        [Range(1, 12)]
        public int ExpiryMonth { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "Amount must be a positive whole number")]
        public string Amount { get; set; }

        [DataType(DataType.Currency)]
        public string Currency { get; set; }

        public string Cvv { get; set; }
    }
}