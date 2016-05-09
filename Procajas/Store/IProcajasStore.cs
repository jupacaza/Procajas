using Procajas.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Procajas.Store
{
    public interface IProcajasStore
    {
        Task<bool> InsertWarehouseResource(WarehouseResource resource);

        Task<List<MaterialLocationQuantity>> GetQuantitiesPerLocation(QuantitiesPerLocationResource resource);

        Task<bool> CheckoutWarehouseResource(List<CheckoutResource> resourceList);

        Task<bool> InsertProcessResource(ProcessResource resource);

        Task<List<ProcessCheckoutConsumedMaterial>> GetMaterialsInProcess(MaterialsInProcessResource resource);

        Task<bool> CheckoutProcessResource(List<CheckoutProcessResource> resourceList);

        Task<bool> InsertFinishedProduct(FinishedProductResource resource);

        Task<bool> InsertDiscrepancyResources(List<DiscrepancyResource> resourceList);

        Task<List<string>> GetAdminItemsByType(AdminItemTypes adminItemType, IDictionary<bool, string> filter = null);

        Task<bool> InsertAdminItemByType(string item, AdminItemTypes adminItemType);

        Task<bool> DeleteAdminItemByType(string item, AdminItemTypes adminItemType);
    }

    public class CheckoutResource
    {
        public string Material { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }
    }

    public class CheckoutProcessResource : CheckoutResource
    {
        public string Process { get; set; }
    }

    public class MaterialsInProcessResource
    {
        public string Process { get; set; }
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

    public class QuantitiesPerLocationResource
    {
        public string Material { get; set; }
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
