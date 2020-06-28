using System.Collections.Generic;
using System.Net;
using System.Threading;
using FluentAssertions;
using MarjiGateway.Application.Models;
using MarjiGateway.SpecificationTests.Facads;
using Swagger.Models;

namespace MarjiGateway.SpecificationTests.Steps
{
    public class CreatePaymentSteps
    {
        private readonly MarjiGatewayApiFacade _marjiGatewayApiFacade;

        public CreatePaymentSteps(MarjiGatewayApiFacade marjiGatewayApiFacade)
        {
            _marjiGatewayApiFacade = marjiGatewayApiFacade;
        }

        public void UserRequestsCreatePaymentWith(PaymentRequest request)
        {
            _marjiGatewayApiFacade.CreatePayment(request, CancellationToken.None).Wait();
        }

        public void TheHttpStatusCodeResponseShouldBe(HttpStatusCode httpStatusCode)
        {
            var lastStatusCode = _marjiGatewayApiFacade.GetLastOperationStatusCode();
            lastStatusCode.Should().NotBeNull();
            lastStatusCode.Should().Be(httpStatusCode);
        }

        public void TheResponseShouldBe(ProcessPaymentResponse paymentResponse)
        {
            var response = (dynamic)_marjiGatewayApiFacade.GetLastOperationResponse();
            var body = (ProcessPaymentResponse)response.Body;
            body.Identifier.Should().Be(paymentResponse.Identifier);
            body.IsSuccess.Should().Be(paymentResponse.IsSuccess);
        }

        public void TheErrorResponseShouldBe(IEnumerable<ErrorModel> errors)
        {
            var lastErrorResponse = _marjiGatewayApiFacade.GetLastErrorOperationResponse();
            lastErrorResponse.Should().NotBeNull();
            lastErrorResponse.Should().BeEquivalentTo(errors);
        }
    }
}