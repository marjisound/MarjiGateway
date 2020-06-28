using System;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MarjiGateway.SpecificationTests.Clients
{
    public class HttpClientDelegatingHandlerAdapter : HttpClientHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _invoke;

        public HttpClientDelegatingHandlerAdapter(HttpMessageHandler innerHandler)
        {
            var sendMethod = innerHandler.GetType()
                .GetMethod("SendAsync", BindingFlags.Instance | BindingFlags.NonPublic, Type.DefaultBinder, new[]
                {
                    typeof(HttpRequestMessage), typeof(CancellationToken)
                }, null);
            _invoke = (request, cancellationToken) => (Task<HttpResponseMessage>)sendMethod.Invoke(innerHandler,
                new object[]
                {
                    request, cancellationToken
                });
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return _invoke(request, cancellationToken);
        }
    }
}