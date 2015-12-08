﻿//
// Copyright (c) Microsoft.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using Microsoft.Azure.Management.SiteRecovery.Models;
using Microsoft.Azure.Management.SiteRecovery;
using Microsoft.Azure.Test;
using System.Net;
using System.Linq;
using Xunit;
using System;


namespace SiteRecovery.Tests
{
    public class GetTests : SiteRecoveryTestsBase
    {
        [Fact]
        public void GetServerTest()
        {
            using (UndoContext context = UndoContext.Current)
            {
                context.Start();
                var client = GetSiteRecoveryClient(CustomHttpHandler);

                FabricListResponse responseServers = client.Fabrics.List(RequestHeaders);

                FabricResponse response = client.Fabrics.Get(responseServers.Fabrics[0].Name, RequestHeaders);

                Assert.NotNull(response.Fabric);
                Assert.NotNull(response.Fabric.Name);
                Assert.NotNull(response.Fabric.Id);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public void GetRecoveryServicesProviders()
        {
            using (UndoContext context = UndoContext.Current)
            {
                context.Start();
                var client = GetSiteRecoveryClient(CustomHttpHandler);

                FabricListResponse responseServers = client.Fabrics.List(RequestHeaders);

                FabricResponse response = client.Fabrics.Get(responseServers.Fabrics[0].Name, RequestHeaders);

                foreach (Fabric fabric in responseServers.Fabrics)
                {
                    if (fabric.Properties.CustomDetails.InstanceType == "VMM")
                    {
                        RecoveryServicesProviderResponse dra =
                            client.RecoveryServicesProvider.Get(fabric.Name, fabric.Properties.InternalIdentifier, RequestHeaders);
                    }
                }

                Assert.NotNull(response.Fabric);
                Assert.NotNull(response.Fabric.Name);
                Assert.NotNull(response.Fabric.Id);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public void GetProtectedContainerTest()
        {
            using (UndoContext context = UndoContext.Current)
            {
                context.Start();
                var client = GetSiteRecoveryClient(CustomHttpHandler);

                var responseServers = client.Fabrics.List(RequestHeaders);

                Fabric selectedfabric = null;
                foreach (var fabric in responseServers.Fabrics)
                {
                    if (fabric.Properties.CustomDetails.InstanceType.Contains("VMM"))
                    {
                        selectedfabric = fabric;
                        break;
                    }
                }

                var protectionContainerList = client.ProtectionContainer.List(selectedfabric.Name, RequestHeaders);
                var response = client.ProtectionContainer.Get(selectedfabric.Name,
                    protectionContainerList.ProtectionContainers[0].Name,
                    RequestHeaders);

                Assert.NotNull(response.ProtectionContainer);
                Assert.NotNull(response.ProtectionContainer.Name);
                Assert.NotNull(response.ProtectionContainer.Id);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public void GetAllProtectedContainerTest()
        {
            using (UndoContext context = UndoContext.Current)
            {
                context.Start();
                var client = GetSiteRecoveryClient(CustomHttpHandler);

                var protectionContainerList = client.ProtectionContainer.ListAll(RequestHeaders);
            }
        }

	[Fact]
        public void GetReplicationProtectedItems()
        {
            using (UndoContext context = UndoContext.Current)
            {
                context.Start();
                var client = GetSiteRecoveryClient(CustomHttpHandler);

                string fabricId = "f0632449-effd-4858-a210-4ea15756e4b7";
                string containerId = "d38048d4-b460-4791-8ece-108395ee8478";

                var response = client.ReplicationProtectedItem.List(fabricId, containerId, RequestHeaders);
            }
        }

        [Fact]
        public void GetVMwareAzureV2ReplicationProtectableItems()
        {
            using (UndoContext context = UndoContext.Current)
            {
                context.Start();
                var client = GetSiteRecoveryClient(CustomHttpHandler);

                var responseServers = client.Fabrics.List(RequestHeaders);

                Assert.True(
                    responseServers.Fabrics.Count > 0,
                    "Servers count can't be less than 1");

                var vmWareFabric = responseServers.Fabrics.First(
                    fabric => fabric.Properties.CustomDetails.InstanceType == "VMware");
                Assert.NotNull(vmWareFabric);

                var containersResponse = client.ProtectionContainer.List(
                    vmWareFabric.Name,
                    RequestHeaders);
                Assert.NotNull(containersResponse);
                Assert.True(
                    containersResponse.ProtectionContainers.Count > 0,
                    "Containers count can't be less than 1.");

                var protectableItemsResponse = client.ProtectableItem.List(
                    vmWareFabric.Name,
                    containersResponse.ProtectionContainers[0].Name,
                    "Unprotected",
                    "",
                    "1000",
                    RequestHeaders);
                Assert.NotNull(protectableItemsResponse);
                Assert.NotEmpty(protectableItemsResponse.ProtectableItems);

                var protectableItem = protectableItemsResponse.ProtectableItems[0];
                Assert.NotNull(protectableItem.Properties.CustomDetails);

                var vmWareAzureV2Details = protectableItem.Properties.CustomDetails
                    as VMwareVirtualMachineDetails;
                Assert.NotNull(vmWareAzureV2Details);

                var hikewalrProtectableItems =
                    protectableItemsResponse.ProtectableItems.Where(
                        p => p.Properties.FriendlyName.Contains("hikewalr"));
            }
        }

        [Fact]
        public void GetVMwareAzureV2ReplicationProtectedItem()
        {
            using (UndoContext context = UndoContext.Current)
            {
                context.Start();
                var client = this.GetSiteRecoveryClient(CustomHttpHandler);

                var response = client.ReplicationProtectedItem.Get(
                    "Vmm;2e908a93-f69f-49fe-bdf8-8687a9f1db35",
                    "cloud_2e908a93-f69f-49fe-bdf8-8687a9f1db35",
                    "6d1a20c4-7e4c-11e5-bbda-0050569e3855-Protected",
                    RequestHeaders);

                Assert.NotNull(response);
                Assert.NotNull(response.ReplicationProtectedItem);
            }
        }

        [Fact]
        public void GetNetworkTest()
        {
            using (UndoContext context = UndoContext.Current)
            {
                context.Start();
                var client = GetSiteRecoveryClient(CustomHttpHandler);

                const string fabricName = "Vmm;f0632449-effd-4858-a210-4ea15756e4b7";
                const string networkName = "15e1a665-e184-4da0-8e9d-0de72a7d8fa9";
                var response = client.Network.Get(fabricName, networkName, RequestHeaders);
            }
        }

        [Fact]
        public void GetNetworkMappingTest()
        {
            using (UndoContext context = UndoContext.Current)
            {
                context.Start();
                var client = GetSiteRecoveryClient(CustomHttpHandler);

                const string fabricName = "Vmm;f0632449-effd-4858-a210-4ea15756e4b7";
                const string networkName = "399137cc-f0de-4a3f-b961-fd0892d8ebc4";
                const string networkMappingName = "VMNetworkPair;f0632449-effd-4858-a210-4ea15756e4b7_399137cc-f0de-4a3f-b961-fd0892d8ebc4_21a9403c-6ec1-44f2-b744-b4e50b792387";
                var response = client.NetworkMapping.Get(
                    fabricName,
                    networkName,
                    networkMappingName,
                    RequestHeaders);
            }
        }

        [Fact]
        public void GetContainerMappings()
        {
            using (UndoContext context = UndoContext.Current)
            {
                context.Start();
                var client = GetSiteRecoveryClient(CustomHttpHandler);

                string fabricId = "Vmm;6adf9420-b02f-4377-8ab7-ff384e6d792f";
                string containerId = "de2e082c-3123-42c5-a867-10dc58950db6";

                var response = client.ProtectionContainerMapping.List(fabricId, containerId, RequestHeaders);
            }
        }

        [Fact]
        public void GetVMwareAzureV2ContainerMapping()
        {
            using (UndoContext context = UndoContext.Current)
            {
                context.Start();
                var client = GetSiteRecoveryClient(CustomHttpHandler);

                var responseServers = client.Fabrics.List(RequestHeaders);

                Assert.True(
                    responseServers.Fabrics.Count > 0,
                    "Servers count can't be less than 1");

                var vmWareFabric = responseServers.Fabrics.First(
                    fabric => fabric.Properties.CustomDetails.InstanceType == "VMware");
                Assert.NotNull(vmWareFabric);

                var containersResponse = client.ProtectionContainer.List(
                    vmWareFabric.Name,
                    RequestHeaders);
                Assert.NotNull(containersResponse);
                Assert.True(
                    containersResponse.ProtectionContainers.Count > 0,
                    "Containers count can't be less than 1.");

                var containersMappingResponse = client.ProtectionContainerMapping.List(
                    vmWareFabric.Name,
                    containersResponse.ProtectionContainers[0].Name,
                    RequestHeaders);
                Assert.NotNull(containersMappingResponse);
            }
        }

        [Fact]
        public void GetEvents()
        {
            using (UndoContext context = UndoContext.Current)
            {
                context.Start();
                var client = GetSiteRecoveryClient(CustomHttpHandler);

                var response = client.Events.List(
                    new EventQueryParameter
                    {
                        StartTime = DateTime.Now.AddDays(-21).ToString(),
                        EndTime = DateTime.Now.AddDays(-7).ToString(),
                        AffectedObjectFriendlyName = "sadko-1102-1",
                        FabricName = "21e443a8cf90841638add43368f2f69db0e8b7511acdbb1c1111e3a75e5095b6"
                    },
                    RequestHeaders);
                Assert.NotNull(response);
                Assert.NotEmpty(response.Events);
            }
        }
    }
}
