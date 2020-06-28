// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Swagger.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    public partial class Payment
    {
        /// <summary>
        /// Initializes a new instance of the Payment class.
        /// </summary>
        public Payment()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Payment class.
        /// </summary>
        public Payment(string cardNumber, int expiryYear, int expiryMonth, string amount, string currency, string cvv)
        {
            CardNumber = cardNumber;
            ExpiryYear = expiryYear;
            ExpiryMonth = expiryMonth;
            Amount = amount;
            Currency = currency;
            Cvv = cvv;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "cardNumber")]
        public string CardNumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "expiryYear")]
        public int ExpiryYear { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "expiryMonth")]
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "cvv")]
        public string Cvv { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {

        }
    }
}
