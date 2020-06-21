using MarjiGateway.Application.Models;
using MarjiGateway.Web.Api.Models;
using Swashbuckle.AspNetCore.Examples;

namespace MarjiGateway.Web.Api.Swagger.Examples
{
    public class ProcessPaymentExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new PaymentRequest()
            {
                Payment = new Payment()
                {
                    Amount = "100",
                    CardNumber = "4242424242424242",
                    Cvv = "434",
                    Currency = Currency.Eur,
                    ExpiryMonth = 3,
                    ExpiryYear = 2021
                }
            };
        }
    }
}