using System;

namespace MarjiGateway.Application.Models
{
    public class PaymentEntity : Payment
    {
        public string Identifier { get; set; }
        public DateTime Created { get; set; }
        public PaymentStatus Status { get; set; }
    }
}