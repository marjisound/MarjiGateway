using System;
using BoDi;
using MarjiGateway.SpecificationTests.Facads;
using MarjiGateway.SpecificationTests.Steps;
using Swagger;

namespace MarjiGateway.SpecificationTests.Configuration
{
    public class InMemoryConfiguration
    {
        public InMemoryConfiguration()
        {
            var container = CreateContainer();
            BankAdapteSteps = container.Resolve<AdapterSetUpSteps>();
            CreatePaymentSteps = container.Resolve<CreatePaymentSteps>();
        }

        public AdapterSetUpSteps BankAdapteSteps { get; set; }
        public CreatePaymentSteps CreatePaymentSteps { get; set; }

        private ObjectContainer CreateContainer()
        {
            var container = new ObjectContainer();
            var baseUri = new Uri("http://localhost");

            container.RegisterInstanceAs<InMemoryTestServer>(new InMemoryTestServer(container));

            container.RegisterInstanceAs<IMarjiGatewayAPI>(
                new TestAPI(
                    baseUri, 
                    container.Resolve<InMemoryTestServer>().GetHandler(),
                    container.Resolve<InMemoryTestServer>().GetHttpClient()));

            container.RegisterInstanceAs(new MarjiGatewayApiFacade(container.Resolve<IMarjiGatewayAPI>()));

            return container;
        }
    }
}