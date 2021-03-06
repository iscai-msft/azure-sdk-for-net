// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.CognitiveServices.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Cognitive Services resource type and SKU.
    /// </summary>
    public partial class CognitiveServicesResourceAndSku
    {
        /// <summary>
        /// Initializes a new instance of the CognitiveServicesResourceAndSku
        /// class.
        /// </summary>
        public CognitiveServicesResourceAndSku()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the CognitiveServicesResourceAndSku
        /// class.
        /// </summary>
        /// <param name="resourceType">Resource Namespace and Type</param>
        /// <param name="sku">The SKU of Cognitive Services account.</param>
        public CognitiveServicesResourceAndSku(string resourceType = default(string), Sku sku = default(Sku))
        {
            ResourceType = resourceType;
            Sku = sku;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets resource Namespace and Type
        /// </summary>
        [JsonProperty(PropertyName = "resourceType")]
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the SKU of Cognitive Services account.
        /// </summary>
        [JsonProperty(PropertyName = "sku")]
        public Sku Sku { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Sku != null)
            {
                Sku.Validate();
            }
        }
    }
}
