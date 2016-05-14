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

        public async Task<bool> CheckoutProcessResource(List<CheckoutProcessResource> resourceList)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckoutWarehouseResource(List<CheckoutResource> resourceList)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAdminItemByType(string item, AdminItemTypes adminItemType)
        {
            return await this.client.DeleteAdminItem(adminItemType.ToString(), item);
        }

        public async Task<List<string>> GetAdminItemsByType(AdminItemTypes adminItemType, IDictionary<bool, string> filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProcessCheckoutConsumedMaterial>> GetMaterialsInProcess(MaterialsInProcessResource resource)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MaterialLocationQuantity>> GetQuantitiesPerLocation(QuantitiesPerLocationResource resource)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAdminItemByType(string item, AdminItemTypes adminItemType)
        {
            Contracts.AdminItemResource contract = new Contracts.AdminItemResource()
            {
                Name = item,
                Type = adminItemType.ToString()
            };

            return await this.client.PostAdminItem(contract);
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

            List<string> ids = await this.client.PostDiscrepancies(contractList);

            // if the list is empty, then the operation was unsuccessful
            return ids.Count == 0 ? false : true;
        }

        public async Task<bool> InsertFinishedProduct(FinishedProductResource resource)
        {
            Contracts.FinishedProductResource contract = new Contracts.FinishedProductResource()
            {
                Material = resource.Material,
                Quantity = resource.Quantity,
                FinishDate = resource.FinishDate,
                Location = resource.Location,
                InvoiceNumber = resource.InvoiceNumber
            };

            return await this.client.PostFinishedProduct(contract);
        }

        public async Task<bool> InsertProcessResource(ProcessResource resource)
        {
            Contracts.ProcessResource contract = new Contracts.ProcessResource()
            {
                Material = resource.Material,
                Quantity = resource.Quantity,
                Department = resource.Department,
                ProcessCheckinDate = resource.ProcessCheckinDate,
                Location = resource.Location,
                Details = resource.Details
            };

            string processId = await this.client.PostProcess(contract);

            return string.IsNullOrEmpty(processId) ? false : true;
        }

        public async Task<bool> InsertWarehouseResource(WarehouseResource resource)
        {
            Contracts.WarehouseResource contract = new Contracts.WarehouseResource()
            {
                Material = resource.Material,
                Quantity = resource.Quantity,
                Department = resource.Department,
                DateOfInsertion = resource.DateOfInsertion,
                Location = resource.Location,
                InvoiceNumber = resource.InvoiceNumber
            };

            string id = await this.client.PostWarehouse(contract);

            return string.IsNullOrEmpty(id) ? false : true;
        }
    }
}
