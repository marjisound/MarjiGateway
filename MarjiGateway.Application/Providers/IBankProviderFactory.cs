using MarjiGateway.Application.Ports;

namespace MarjiGateway.Application.Providers
{
    public interface IBankProviderFactory
    {
        IBankAdapter Create(string bankProvider);
    }
}