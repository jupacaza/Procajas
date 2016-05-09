using Procajas.Clients;
using Procajas.Contracts;
using Procajas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Procajas.Store
{
    public class ProcajasServiceStore : IProcajasStore
    {
        private ProcajasServiceClient client = new ProcajasServiceClient();

        public Task<bool> CheckoutProcessResource(List<CheckoutProcessResource> resourceList)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckoutWarehouseResource(List<CheckoutResource> resourceList)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAdminItemByType(string item, AdminItemTypes adminItemType)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAdminItemsByType(AdminItemTypes adminItemType, IDictionary<bool, string> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProcessCheckoutConsumedMaterial>> GetMaterialsInProcess(MaterialsInProcessResource resource)
        {
            throw new NotImplementedException();
        }

        public Task<List<MaterialLocationQuantity>> GetQuantitiesPerLocation(QuantitiesPerLocationResource resource)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAdminItemByType(string item, AdminItemTypes adminItemType)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertDiscrepancyResources(List<DiscrepancyResource> resourceList)
        {
            List<Contracts.DiscrepancyResource> contractList = resourceList.Select(
                (resource) =>
                {
                    Contracts.DiscrepancyResource contract = new Contracts.DiscrepancyResource()
                    {
                        Material = resource.Material,
                        Department = resource.Department,
                        Quantity = resource.Quantity,
                        DiscrepancyDate = resource.DiscrepancyDate,
                        Job = resource.Job
                    };

                    return contract;
                }
            ).ToList();

            List<string> ids = await this.client.PostDiscrepancyResource(contractList);

            // check if any id reports a client error
            if (ids.Any(id => id.Equals(ProcajasServiceClient.ClientError)))
            {
                return false;
            }

            return true;
        }

        public Task<bool> InsertFinishedProduct(FinishedProductResource resource)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertProcessResource(ProcessResource resource)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertWarehouseResource(WarehouseResource resource)
        {
            throw new NotImplementedException();
        }
    }
}
