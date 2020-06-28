using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarjiGateway.Application.Models
{
    public class Payment : IValidatableObject
    {
        [Required]
        [CreditCard]
        public string CardNumber { get; set; }

        [Required]
        [RegularExpression("^\\d{4}$", ErrorMessage = "Expiry year is not valid")]
        public int ExpiryYear { get; set; }

        [Required]
        [RegularExpression("^(0?[1-9]|1[012])$", ErrorMessage = "Expiry month is not valid")]
        public int ExpiryMonth { get; set; }

        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Amount must be a positive whole number")]
        public string Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        [RegularExpression("^[0-9]{3,4}$", ErrorMessage = "cvv is not valid")]
        public string Cvv { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Constants.CurrencyTypes.Contains(Currency.ToUpper()))
            {
                yield return new ValidationResult(
                    $"Currency is not valid. Only following currencies are supported {string.Join(",", Constants.CurrencyTypes)}",
                    new[] { nameof(Currency) });
            }

            if (ExpiryYear < DateTime.Now.Year)
            {
                yield return new ValidationResult(
                    $"Expiry year must be greater than or equal to {DateTime.Now.Year}",
                    new[] { nameof(ExpiryYear) });
            }

            if (ExpiryYear == DateTime.Now.Year && ExpiryMonth < DateTime.Now.Month)
            {
                yield return new ValidationResult(
                    $"Expiry month must be greater than or equal to {DateTime.Now.Month}",
                    new[] { nameof(ExpiryMonth) });
            }
        }
    }
}