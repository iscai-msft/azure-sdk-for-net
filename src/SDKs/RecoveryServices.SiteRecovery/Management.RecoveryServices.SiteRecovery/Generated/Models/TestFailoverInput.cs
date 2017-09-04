// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.RecoveryServices.SiteRecovery.Models
{
    using Microsoft.Azure;
    using Microsoft.Azure.Management;
    using Microsoft.Azure.Management.RecoveryServices;
    using Microsoft.Azure.Management.RecoveryServices.SiteRecovery;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Input definition for planned failover.
    /// </summary>
    public partial class TestFailoverInput
    {
        /// <summary>
        /// Initializes a new instance of the TestFailoverInput class.
        /// </summary>
        public TestFailoverInput()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TestFailoverInput class.
        /// </summary>
        /// <param name="properties">Planned failover input properties</param>
        public TestFailoverInput(TestFailoverInputProperties properties = default(TestFailoverInputProperties))
        {
            Properties = properties;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets planned failover input properties
        /// </summary>
        [JsonProperty(PropertyName = "properties")]
        public TestFailoverInputProperties Properties { get; set; }

    }
}