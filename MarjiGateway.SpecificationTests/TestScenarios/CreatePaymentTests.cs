using System;
using System.Collections.Generic;
using System.Net;
using MarjiGateway.Application.Models;
using MarjiGateway.SpecificationTests.Configuration;
using MarjiGateway.SpecificationTests.Steps;
using NUnit.Framework;
using Swagger.Models;
using TestStack.BDDfy;
using Payment = Swagger.Models.Payment;
using ProcessPaymentResponse = MarjiGateway.Application.RequestHandlers.ProcessPayment.ProcessPaymentResponse;

namespace MarjiGateway.SpecificationTests.TestScenarios
{
    public class CreatePaymentTests
    {
        private AdapterSetUpSteps _adapterSetUpSteps;
        private CreatePaymentSteps _createPaymentSteps;

        [SetUp]
        public void SetUp()
        {
            var config = new InMemoryConfiguration();
            _adapterSetUpSteps = config.BankAdapteSteps;
            _createPaymentSteps = config.CreatePaymentSteps;
        }

        [Test]
        public void CreateAllocationDefaultCase()
        {
            var expectedIdentifier = Guid.NewGuid().ToString();
            this.Given(_ => _adapterSetUpSteps.BankAdapterReturnsProcessedPayment(new ProcessPaymentResponse(){IsSuccess = true, Identifier = expectedIdentifier }))
                .And(_ => _adapterSetUpSteps.BankFinderReturns("halifax"))
                .When(_ => _createPaymentSteps.UserRequestsCreatePaymentWith(
                    new PaymentRequest(
                        new Payment("Mary Kalan","4242424242424242", 2021, 2, "254", "EUR", "678"))))
                .Then(_ => _createPaymentSteps.TheHttpStatusCodeResponseShouldBe(HttpStatusCode.OK))
                .And(_ => _createPaymentSteps.TheResponseShouldBe(new Swagger.Models.ProcessPaymentResponse(){IsSuccess = true, Identifier = expectedIdentifier }))
                .BDDfy();
        }

        [Test]
        [TestCaseSource(nameof(TestCaseSourceDataForModelValidationError))]
        public void CreateAllocationModelValidationError(List<ErrorModel> errors, Payment payment)
        {
            var expectedIdentifier = Guid.NewGuid().ToString();

            this.Given(_ => _adapterSetUpSteps.BankAdapterReturnsProcessedPayment(new ProcessPaymentResponse() { IsSuccess = true, Identifier = expectedIdentifier }))
                .And(_ => _adapterSetUpSteps.BankFinderReturns("halifax"))
                .When(_ => _createPaymentSteps.UserRequestsCreatePaymentWith(new PaymentRequest(payment)))
                .Then(_ => _createPaymentSteps.TheHttpStatusCodeResponseShouldBe(HttpStatusCode.BadRequest))
                .And(_ => _createPaymentSteps.TheErrorResponseShouldBe(errors))
                .BDDfy();
        }

        // TODO: more test cases need to be added here
        private static IEnumerable<TestCaseData> TestCaseSourceDataForModelValidationError()
        {
            yield return new TestCaseData(
                new List<ErrorModel>(){new ErrorModel(){ErrorMessage = "The CardNumber field is not a valid credit card number.", ErrorCode = "ModelValidationError", ParameterName = "Payment.CardNumber" }},
                new Payment("Mary Kalan", "57289797915i8797", 2021, 2, "254", "EUR", "678"));
            yield return new TestCaseData(
                new List<ErrorModel>() { new ErrorModel() { ErrorMessage = "Expiry year must be greater than or equal to 2020", ErrorCode = "ModelValidationError", ParameterName = "Payment.ExpiryYear" } },
                new Payment("Mary Kalan", "4242424242424242", 2019, 2, "254", "EUR", "678"));
        }
    }
}