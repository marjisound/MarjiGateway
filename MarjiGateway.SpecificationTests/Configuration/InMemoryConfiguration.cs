using System;
using BoDi;

namespace MarjiGateway.SpecificationTests.Configuration
{
    public class InMemoryConfiguration
    {
        public InMemoryConfiguration()
        {
            var container = new ObjectContainer();
        }

        private ObjectContainer CreateContainer()
        {
            var container = new ObjectContainer();
            var baseUri = new Uri("http://localhost");

            container.RegisterInstanceAs<InMemoryTestServer>(new InMemoryTestServer(container));

            return container;
        }
    }
}