using Procajas.Clients;
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

        public async Task<List<string>> CheckoutFromProcess(List<CheckoutProcessResource> resourceList)
        {
            bool success = true;
            Dictionary<string, Contracts.ProcessResource> resourcesToUpdate = new Dictionary<string, Contracts.ProcessResource>();

            // go through all the resources
            foreach (CheckoutProcessResource resource in resourceList)
            {
                // get the specific item from the warehouse
                Contracts.ProcessResource itemInStore = await this.client.GetProcessResourceByDepartmentAndId(Utilities.GetDepartmentFromMaterial(resource.Material), resource.Id);

                // compare quantity in store and quantity to use
                if (resource.Quantity > itemInStore.Quantity)
                {
                    // if the quantity to use is more than the quantity in store: fail and do not update anything
                    success = false;
                    break;
                }

                // substract quantity to use from quantity in store and prepare the record to update in store
                Contracts.ProcessResource itemToUpdate = itemInStore.Copy();
                itemToUpdate.Quantity = itemInStore.Quantity - resource.Quantity;
                resourcesToUpdate.Add(itemToUpdate.Id, itemToUpdate);
            }

            List<string> updatedIds = new List<string>();
            if (success)
            {
                // do the update of each resource
                foreach (KeyValuePair<string, Contracts.ProcessResource> resourceToUpdate in resourcesToUpdate)
                {
                    string updatedId = await this.client.PutProcess(resourceToUpdate.Key, resourceToUpdate.Value);
                    if (!string.IsNullOrEmpty(updatedId))
                    {
                        // only add the Ids that succeeded to update
                        updatedIds.Add(updatedId);
                    }
                }
            }

            return updatedIds;
        }

        public async Task<List<string>> CheckoutFromWarehouse(List<CheckoutWarehouseResource> resourceList)
        {
            bool success = true;
            Dictionary<string, Contracts.WarehouseResource> resourcesToUpdate = new Dictionary<string, Contracts.WarehouseResource>();

            // go through all the resources
            foreach(CheckoutWarehouseResource resource in resourceList)
            {
                // get the specific item from the warehouse
                Contracts.WarehouseResource itemInStore = await this.client.GetWarehouseResourceByDepartmentAndId(Utilities.GetDepartmentFromMaterial(resource.Material), resource.Id);

                // compare quantity in store and quantity to use
                if (resource.Quantity > itemInStore.Quantity)
                {
                    // if the quantity to use is more than the quantity in store: fail and do not update anything
                    success = false;
                    break;
                }

                // substract quantity to use from quantity in store and prepare the record to update in store
                Contracts.WarehouseResource itemToUpdate = itemInStore.Copy();
                itemToUpdate.Quantity = itemInStore.Quantity - resource.Quantity;
                resourcesToUpdate.Add(itemToUpdate.Id, itemToUpdate);
            }

            List<string> updatedIds = new List<string>();
            if (success)
            {
                // do the update of each resource
                foreach(KeyValuePair<string, Contracts.WarehouseResource> resourceToUpdate in resourcesToUpdate)
                {
                    string updatedId = await this.client.PutWarehouse(resourceToUpdate.Key, resourceToUpdate.Value);
                    if (!string.IsNullOrEmpty(updatedId))
                    {
                        // only add the Ids that succeeded to update
                        updatedIds.Add(updatedId);
                    }
                }
            }

            return updatedIds;
        }

        public async Task<bool> DeleteAdminItemByType(string item, AdminItemTypes adminItemType)
        {
            return await this.client.DeleteAdminItem(adminItemType.ToString(), item);
        }

        public async Task<List<string>> GetAdminItemsByType(AdminItemTypes adminItemType, IDictionary<string, bool> filter = null)
        {
            List<string> adminItemsByType = await this.client.ListAdminItemsByType(adminItemType.ToString());
            if (filter == null)
            {
                return adminItemsByType;
            }
            
            // go through the adminItems and pick only the ones where:
            // 1. the value of the filter says true and the material name starts with the key of the filter
            // 2. the value of the filter says false and the material does not start with the key of the filter
            return adminItemsByType.Where(
                s =>
                {
                    // go through each value of the filter
                    foreach (KeyValuePair<string, bool> kvp in filter)
                    {
                        // XOR will give true when the values are different. We know it's a match when the boolean values are the same, hence the NOT.
                        if (!string.IsNullOrEmpty(kvp.Key) && !(kvp.Value ^ s.StartsWith(kvp.Key, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            ).ToList();
        }
        
        public async Task<List<ProcessCheckoutConsumedMaterial>> GetMaterialsInProcess(string process)
        {
            List<ProcessCheckoutConsumedMaterial> processMaterials;
            List<Contracts.ProcessResource> processResources = await this.client.ListProcessResourcesByDepartment(process);
            if (processResources.Count > 0)
            {
                processMaterials = processResources.Select(
                    r =>
                    {
                        ProcessCheckoutConsumedMaterial processMaterial = new ProcessCheckoutConsumedMaterial()
                        {
                            Id = r.Id,
                            Material = r.Material,
                            Existing = r.Quantity
                        };

                        return processMaterial;
                    }).ToList();
            }
            else
            {
                processMaterials = new List<ProcessCheckoutConsumedMaterial>();
            }

            return processMaterials;
        }

        public async Task<List<MaterialLocationQuantity>> GetQuantitiesPerLocationOfMaterial(string material)
        {
            // obtain the department (process name) from the material name
            string department = Utilities.GetDepartmentFromMaterial(material);
            List<Contracts.WarehouseResource> warehouseResources = await this.client.ListWarehouseResourcesByDepartment(department);

            List<MaterialLocationQuantity> materialLocationQuantities = warehouseResources
                // first filter out the materials we are not looking for
                .Where(wr => string.Equals(wr.Material, material))

                // then transform to the mlq format
                .Select(wr => new MaterialLocationQuantity()
                {
                    Id = wr.Id,
                    Material = wr.Material,
                    ExistingQuantity = wr.Quantity,
                    Location = wr.Location
                }).ToList();

            return materialLocationQuantities;
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
