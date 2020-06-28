using System;
using System.Net.Http;
using MarjiGateway.SpecificationTests.Clients;

namespace Swagger
{
    public class TestAPI : MarjiGatewayAPI
    {
        public TestAPI(Uri baseUri,
            HttpMessageHandler rootHandler,
            HttpClient client) :
            base(baseUri, new HttpClientDelegatingHandlerAdapter(rootHandler))
        {
            HttpClient = client;
        }
    }
}