using System.Collections.Generic;
using MarjiGateway.Adapters.BankAdapters;
using MarjiGateway.Adapters.BankFinder;
using MarjiGateway.Adapters.Repositories;
using MarjiGateway.Application.Ports;
using MarjiGateway.Application.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace MarjiGateway.Web.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            return serviceCollection
                .AddRepositories()
                .AddBankFinder()
                .AddHsbc()
                .AddSingleton<IBankProviderFactory, BankProviderFactory>(collection =>
                {
                    var banks = new Dictionary<string, IBankAdapter>()
                    {
                        ["hsbc"] = collection.GetRequiredService<HsbcBankAdapter>()
                    };

                    return new BankProviderFactory(banks);
                });
        }

        public static IServiceCollection AddBankFinder(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IBankFinderAdapter, BankFinder>();
            return serviceCollection;
        }

        public static IServiceCollection AddHsbc(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<HsbcBankAdapter>();
            return serviceCollection;
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IPaymentRepository, PaymentRepository>();
            return serviceCollection;
        }
    }
}