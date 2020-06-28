using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MarjiGateway.Application.Models;
using Microsoft.Rest;
using Newtonsoft.Json;
using Swagger;
using Swagger.Models;

namespace MarjiGateway.SpecificationTests.Facads
{
    public class MarjiGatewayApiFacade
    {
        private readonly IMarjiGatewayAPI _marjiGatewayApi;
        private List<HttpOperationResponse> _history;
        private List<Exception> _exceptionHistory;
        private List<List<ErrorModel>> _errorHistory;
        private List<HttpStatusCode?> _statusCodeHistory;

        public MarjiGatewayApiFacade(IMarjiGatewayAPI marjiGatewayApi)
        {
            _marjiGatewayApi = marjiGatewayApi;
            _history = new List<HttpOperationResponse>();
            History = new ReadOnlyCollection<HttpOperationResponse>(_history);

            _exceptionHistory = new List<Exception>();
            ExceptionHistory = new ReadOnlyCollection<Exception>(_exceptionHistory);

            _errorHistory = new List<List<ErrorModel>>();
            ErrorHistory = new ReadOnlyCollection<List<ErrorModel>>(_errorHistory);

            _statusCodeHistory = new List<HttpStatusCode?>();
            StatusCodeHistory = new ReadOnlyCollection<HttpStatusCode?>(_statusCodeHistory);
        }

        public ReadOnlyCollection<HttpStatusCode?> StatusCodeHistory { get; set; }

        public ReadOnlyCollection<Exception> ExceptionHistory { get; set; }
        public ReadOnlyCollection<List<ErrorModel>> ErrorHistory { get; set; }

        public ReadOnlyCollection<HttpOperationResponse> History { get; set; }

        public Task<HttpOperationResponse<ProcessPaymentResponse>> CreatePayment(PaymentRequest request, CancellationToken cancellationToken)
        {
            return InvokeAsync(() =>
                _marjiGatewayApi.ProcessPaymentWithHttpMessagesAsync(request, cancellationToken: cancellationToken));
        }

        public HttpStatusCode? GetLastOperationStatusCode()
        {
            return _statusCodeHistory.LastOrDefault();
        }

        public List<ErrorModel> GetLastErrorOperationResponse()
        {
            return _errorHistory.LastOrDefault();
        }

        private List<ErrorModel> GetLastErrorOperationResponse(AggregateException exception)
        {
            foreach (var innerException in exception.InnerExceptions)
            {
                var errorModels = GetLastErrorOperationResponse((dynamic)innerException);
                if (errorModels != null)
                {
                    return errorModels;
                }
            }

            return null;
        }

        public HttpOperationResponse GetLastOperationResponse()
        {
            if (!_history.Any())
            {
                return null;
            }

            return _history.Last();
        }

        public HttpOperationResponse<T> GetLastOperationResponse<T>()
        {
            var lastOperationResponse = GetLastOperationResponse();
            var operationResponse = lastOperationResponse as HttpOperationResponse<T>;
            return operationResponse;
        }

        protected async Task<HttpOperationResponse<T>> InvokeAsync<T>(Func<Task<HttpOperationResponse<T>>> invokeAction)
        {
            try
            {
                var httpOperationResponse = await invokeAction();
                _history.Add(httpOperationResponse);
                _statusCodeHistory.Add(httpOperationResponse.Response.StatusCode);
                return httpOperationResponse;
            }
            catch (HttpOperationException ex)
            {
                var error = JsonConvert.DeserializeObject<List<ErrorModel>>(ex.Response.Content);

                if (error != null)
                {
                    _errorHistory.Add(error);
                    _statusCodeHistory.Add(ex.Response.StatusCode);
                }
                else
                {
                    _exceptionHistory.Add(ex);
                    _statusCodeHistory.Add(ex.Response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _statusCodeHistory.Add(null);
                _exceptionHistory.Add(ex);
            }
            return null;
        }
    }
}