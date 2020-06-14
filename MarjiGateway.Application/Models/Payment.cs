namespace MarjiGateway.Application.Models
{
    public class Payment
    {
        public long CardNumber { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryDay { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public int Cvv { get; set; }
    }
}