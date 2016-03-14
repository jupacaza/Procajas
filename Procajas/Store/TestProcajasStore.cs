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

        public Task<bool> CheckoutProcessResource(List<CheckoutProcessResource> resourceList)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CheckoutWarehouseResource(List<CheckoutResource> resourceList)
        {
            return Task.FromResult(true);
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
    }
}
