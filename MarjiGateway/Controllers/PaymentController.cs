using System.Threading;
using MarjiGateway.Web.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarjiGateway.Web.Api.Controllers
{
    [Route("marjigateway/v1/payment")]
    [Produces("application/json")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public PaymentController()
        {
            
        }

        [HttpPost]
        public void ProcessPayment([FromBody] PaymentRequest request, CancellationToken cancellationToken)
        {
            
        }
    }
}