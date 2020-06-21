using System.ComponentModel.DataAnnotations;

namespace MarjiGateway.Application.Models
{
    public class Payment
    {
        [Required]
        [CreditCard]
        public string CardNumber { get; set; }

        [Required]
        [RegularExpression("^\\d{4}$", ErrorMessage = "Expiry year is not valid")]
        [Range(2020, 2500)]
        public int ExpiryYear { get; set; }

        [Required]
        [RegularExpression("^(0?[1-9]|1[012])$", ErrorMessage = "Expiry month is not valid")]
        public int ExpiryMonth { get; set; }

        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Amount must be a positive whole number")]
        public string Amount { get; set; }

        [Required]
        public Currency Currency { get; set; }

        [Required]
        [RegularExpression("^[0-9]{3,4}$", ErrorMessage = "cvv is not valid")]
        public string Cvv { get; set; }
    }
}