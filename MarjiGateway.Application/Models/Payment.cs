using System;
using System.ComponentModel.DataAnnotations;

namespace MarjiGateway.Application.Models
{
    public class Payment
    {
        [Required]
        [CreditCard]
        public string CardNumber { get; set; }

        [Required]
        public int ExpiryYear { get; set; }

        [Required]
        public int ExpiryMonth { get; set; }

        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Amount must be a positive whole number")]
        public string Amount { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public string Currency { get; set; }

        [Required]
        public string Cvv { get; set; }
    }
}