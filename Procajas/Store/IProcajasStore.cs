using Procajas.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Procajas.Store
{
    public interface IProcajasStore
    {
        Task<bool> InsertWarehouseResource(WarehouseResource resource);

        /// <summary>
        /// This method gets the quantites and locations of the given material in the Warehouse.
        /// </summary>
        /// <param name="material">The material to query for in the Warehouse.</param>
        /// <returns>A list of material, location, quantity items describing the existing numbers of this material.</returns>
        Task<List<MaterialLocationQuantity>> GetQuantitiesPerLocationOfMaterial(string material);

        Task<List<string>> CheckoutFromWarehouse(List<CheckoutWarehouseResource> resourceList);

        Task<bool> InsertProcessResource(ProcessResource resource);

        Task<List<ProcessCheckoutConsumedMaterial>> GetMaterialsInProcess(string process);

        Task<List<string>> CheckoutFromProcess(List<CheckoutProcessResource> resourceList);

        Task<bool> InsertFinishedProduct(FinishedProductResource resource);

        Task<bool> InsertDiscrepancyResources(List<DiscrepancyResource> resourceList);

        Task<List<string>> GetAdminItemsByType(AdminItemTypes adminItemType, IDictionary<string, bool> filter = null);

        Task<bool> InsertAdminItemByType(string item, AdminItemTypes adminItemType);

        Task<bool> DeleteAdminItemByType(string item, AdminItemTypes adminItemType);
    }

    public class CheckoutWarehouseResource
    {
        public string Id { get; set; }
        public string Material { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }
    }

    public class CheckoutProcessResource
    {
        public string Id { get; set; }
        public string Material { get; set; }
        public string Process { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }
    }

    public class WarehouseResource
    {
        public string Material { get; set; }
        public string Department { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOfInsertion { get; set; }
        public string Location { get; set; }
        public string InvoiceNumber { get; set; }
    }

    public class ProcessResource
    {
        public string Material { get; set; }
        public string Department { get; set; }
        public int Quantity { get; set; }
        public DateTime ProcessCheckinDate { get; set; }
        public string Location { get; set; }
        public IDictionary<object,object> Details { get; set; }
    }

    public class FinishedProductResource
    {
        public string Material { get; set; }
        public int Quantity { get; set; }
        public DateTime FinishDate { get; set; }
        public string Location { get; set; }
        public string InvoiceNumber { get; set; }
    }

    public class DiscrepancyResource
    {
        public string Material { get; set; }
        public string Department { get; set; }
        public int Quantity { get; set; }
        public DateTime DiscrepancyDate { get; set; }
        public string Job { get; set; }
    }
}
