using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MarjiGateway.Application.Exceptions;
using MarjiGateway.Application.Models;
using MarjiGateway.Application.RequestHandlers.GetPayment;
using MarjiGateway.Application.RequestHandlers.ProcessPayment;
using MarjiGateway.Web.Api.Models;
using MarjiGateway.Web.Api.Swagger.Examples;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Examples;

namespace MarjiGateway.Web.Api.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(ProcessPaymentResponse), (int)HttpStatusCode.OK)]
        [SwaggerRequestExample(typeof(PaymentRequest), typeof(ProcessPaymentExample))]
        [SwaggerOperation("processPayment")]
        [Route("marjigateway/v1/payment")]
        [HttpPost]
        public async Task<ProcessPaymentResponse> ProcessPayment([FromBody] PaymentRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelValidationException(CreateErrorMessage(ModelState));
            }
            return await _mediator.Send(new ProcessPayment {Payment = request.Payment}, cancellationToken);
        }

        [ProducesResponseType(typeof(GetPaymentResponse), (int)HttpStatusCode.OK)]
        [SwaggerRequestExample(typeof(GetPaymentQuery), typeof(GetPaymentQueryExample))]
        [SwaggerOperation("processPayment")]
        [Route("marjigateway/v1/payment")]
        [HttpGet]
        public async Task<GetPaymentResponse> GetPayment([Required]string identifier, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelValidationException(CreateErrorMessage(ModelState));
            }
            return await _mediator.Send(new GetPaymentQuery(){Identifier = identifier}, cancellationToken);
        }

        private IEnumerable<ErrorModel> CreateErrorMessage(ModelStateDictionary modelState)
        {
            var errors = new List<ErrorModel>();

            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;
                var fieldErrors = erroneousField.Errors
                    .Select(error =>
                        new ErrorModel()
                        {
                            ErrorMessage = error.ErrorMessage,
                            ErrorCode = "ModelValidationError",
                            Level = ErrorLevelModel.Error,
                            ParameterName = fieldKey
                        });

                errors.AddRange(fieldErrors);
            }

            return errors;
        }
    }
}