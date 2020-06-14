﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MarjiGateway.Application.Models;
using Newtonsoft.Json;

namespace MarjiGateway.Web.Api.Models
{
    public class PaymentRequest
    {
        [JsonProperty("payment")]
        public Payment Payment { get; set; }
    }
}