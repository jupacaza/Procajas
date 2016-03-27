using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Procajas.Models;

namespace Procajas.Store
{
    public class TestProcajasStore : IProcajasStore
    {
        private Dictionary<string, List<ProcessCheckoutConsumedMaterial>> MaterialsInProcessDictionary = new Dictionary<string, List<ProcessCheckoutConsumedMaterial>>();
        private Dictionary<string, Dictionary<string, int>> QuantitiesPerLocationPerMaterial = new Dictionary<string, Dictionary<string, int>>();
        private List<string> Processes = new List<string>()
        {
            "IMP",
            "SUA",
            "COR"
        };

        private List<string> Materials = new List<string>();
        private List<string> Locations = new List<string>();

        public Task<bool> CheckoutProcessResource(List<CheckoutProcessResource> resourceList)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CheckoutWarehouseResource(List<CheckoutResource> resourceList)
        {
            return Task.FromResult(true);
        }

        public Task<List<string>> GetAdminItemsByType(AdminItemTypes adminItemType, IDictionary<string, string> filter = null)
        {
            switch (adminItemType)
            {
                case AdminItemTypes.Material:
                    return Task.FromResult(this.Materials);
                    
                case AdminItemTypes.Process:
                    return Task.FromResult(this.Processes);

                case AdminItemTypes.Location:
                    return Task.FromResult(this.Locations);
            }

            return Task.FromResult(new List<string>());
        }

        public Task<List<ProcessCheckoutConsumedMaterial>> GetMaterialsInProcess(MaterialsInProcessResource resource)
        {
            List<ProcessCheckoutConsumedMaterial> pccList = new List<ProcessCheckoutConsumedMaterial>()
            {
                new ProcessCheckoutConsumedMaterial { Material = "Agua", Existing = 3000 },
                new ProcessCheckoutConsumedMaterial { Material = "Aire", Existing = 2000 },
                new ProcessCheckoutConsumedMaterial { Material = "Tierra", Existing = 1000 },
                new ProcessCheckoutConsumedMaterial { Material = "Fuego", Existing = 500 },
            };

            return Task.FromResult(pccList);
        }

        public Task<List<MaterialLocationQuantity>> GetQuantitiesPerLocation(QuantitiesPerLocationResource resource)
        {
            List<MaterialLocationQuantity> mlqList = new List<MaterialLocationQuantity>();

            return Task.FromResult(mlqList);
        }

        public Task<bool> InsertAdminItemByType(string item, AdminItemTypes adminItemType)
        {
            switch (adminItemType)
            {
                case AdminItemTypes.Material:
                    this.Materials.Add(item);
                    break;
                case AdminItemTypes.Process:
                    this.Processes.Add(item);
                    break;
                case AdminItemTypes.Location:
                    this.Locations.Add(item);
                    break;
            }

            return Task.FromResult(true);
        }

        public Task<bool> InsertDiscrepanciesResources(List<DiscrepanciesResource> resourceList)
        {
            return Task.FromResult(true);
        }

        public Task<bool> InsertFinishedProduct(FinishedProductResource resource)
        {
            return Task.FromResult(true);
        }

        public Task<bool> InsertProcessResource(ProcessResource resource)
        {
            return Task.FromResult(true);
        }

        public Task<bool> InsertWarehouseResource(WarehouseResource resource)
        {
            Dictionary<string, int> quantitesPerMaterial;
            if (QuantitiesPerLocationPerMaterial.TryGetValue(resource.Location, out quantitesPerMaterial))
            {
                int quantity;
                if (quantitesPerMaterial.TryGetValue(resource.Material, out quantity))
                {
                    quantity += resource.Quantity;
                    quantitesPerMaterial[resource.Material] = quantity;
                }
                else
                {
                    quantitesPerMaterial[resource.Material] = resource.Quantity;
                }
                
            }
            else
            {
                QuantitiesPerLocationPerMaterial[resource.Location] = new Dictionary<string, int>() { { resource.Material, resource.Quantity } };
            }

            return Task.FromResult(true);
        }

        public Task<bool> DeleteAdminItemByType(string item, AdminItemTypes adminItemType)
        {
            switch (adminItemType)
            {
                case AdminItemTypes.Material:
                    this.Materials.Remove(item);
                    break;
                case AdminItemTypes.Process:
                    this.Processes.Remove(item);
                    break;
                case AdminItemTypes.Location:
                    this.Locations.Remove(item);
                    break;
            }

            return Task.FromResult(true);
        }
    }
}
