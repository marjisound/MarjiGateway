using System;
using System.Net.Http;
using System.Reflection;
using BoDi;
using MarjiGateway.Web.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarjiGateway.SpecificationTests.Configuration
{
    public class InMemoryTestServer
    {
        private readonly IObjectContainer _container;
        private HttpClient _httpClient;
        private HttpMessageHandler _httpMessageHandler;

        public InMemoryTestServer(IObjectContainer container)
        {
            _container = container;
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var webHostBuilder = new WebHostBuilder();

            webHostBuilder.UseConfiguration(configuration)
                .ConfigureServices(x => x.AddSingleton<IObjectContainer>(serviceProvider => container))
                .UseSetting(WebHostDefaults.ApplicationKey, typeof(Program).GetTypeInfo().Assembly.FullName);

            TestServer = new TestServer(webHostBuilder);

            _httpClient = GetHttpClient();
        }

        public readonly TestServer TestServer;

        // TODO
        public HttpClient GetHttpClient()
        {
            if (_httpClient == null)
            {
                var httpClient = TestServer.CreateClient();
                httpClient.BaseAddress = new Uri("http://localhost");
                _httpClient = httpClient;
            }

            return _httpClient;
        }

        public HttpMessageHandler GetHandler()
        {
            if (_httpMessageHandler == null)
            {
                var httpMessageHandler = TestServer.CreateHandler();
                _httpMessageHandler = httpMessageHandler;
            }

            return _httpMessageHandler;
        }

        public void Dispose()
        {
            _httpMessageHandler.Dispose();
            TestServer.Dispose();
        }
    }
}