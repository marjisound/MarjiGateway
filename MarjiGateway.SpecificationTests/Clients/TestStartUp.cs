using System.Collections.Generic;
using BoDi;
using MarjiGateway.Application.Ports;
using MarjiGateway.Application.Providers;
using MarjiGateway.SpecificationTests.Facads;
using MarjiGateway.Web.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace MarjiGateway.SpecificationTests.Clients
{
    public class TestStartUp : Startup
    {
        private readonly IObjectContainer _testContainer;
        private readonly IConfiguration _configuration;

        public TestStartUp(IConfiguration configuration, IObjectContainer testContainer) : base(configuration)
        {
            _testContainer = testContainer;
            _configuration = configuration;
        }

        protected override void UpdateServiceConfiguration(IServiceCollection services)
        {
            var mockHsbcAdapter = new Mock<IBankAdapter>();
            _testContainer.RegisterInstanceAs(mockHsbcAdapter);

            _testContainer.RegisterInstanceAs(new BankAdapterFacade(mockHsbcAdapter));

            services.AddSingleton<IBankProviderFactory, BankProviderFactory>(x =>
            {
                var banks = new Dictionary<string, IBankAdapter>()
                {
                    ["hsbc"] = mockHsbcAdapter.Object
                };
               
                return new BankProviderFactory(banks);
            });
        }
    }
}