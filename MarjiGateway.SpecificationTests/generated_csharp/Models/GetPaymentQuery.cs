// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Swagger.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class GetPaymentQuery
    {
        /// <summary>
        /// Initializes a new instance of the GetPaymentQuery class.
        /// </summary>
        public GetPaymentQuery()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the GetPaymentQuery class.
        /// </summary>
        public GetPaymentQuery(string identifier = default(string))
        {
            Identifier = identifier;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "identifier")]
        public string Identifier { get; set; }

    }
}