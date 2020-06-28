using System.Collections.Generic;
using BoDi;
using MarjiGateway.Application.Ports;
using MarjiGateway.Application.Providers;
using MarjiGateway.SpecificationTests.Facads;
using MarjiGateway.Web.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace MarjiGateway.SpecificationTests.Clients
{
    public class TestStartUp : Startup
    {
        private readonly IObjectContainer _testContainer;

        public TestStartUp(IConfiguration configuration, IObjectContainer testContainer) : base(configuration)
        {
            _testContainer = testContainer;
        }

        protected override void UpdateServiceConfiguration(IServiceCollection services)
        {
            var mockHsbcAdapter = new Mock<IBankAdapter>();
            var mockBankFinderAdapter = new Mock<IBankFinderAdapter>();
            _testContainer.RegisterInstanceAs(mockHsbcAdapter);

            _testContainer.RegisterInstanceAs(new BankAdapterFacade(mockHsbcAdapter));
            _testContainer.RegisterInstanceAs(new BankFinderFacade(mockBankFinderAdapter));

            services.AddSingleton<IBankProviderFactory, BankProviderFactory>(x =>
            {
                var banks = new Dictionary<string, IBankAdapter>()
                {
                    ["halifax"] = mockHsbcAdapter.Object
                };
               
                return new BankProviderFactory(banks);
            });

            services.AddSingleton<IBankFinderAdapter>(x => mockBankFinderAdapter.Object);
        }
    }
}