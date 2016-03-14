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
            List<MaterialLocationQuantity> mlqList = new List<MaterialLocationQuantity>()
            {
                new MaterialLocationQuantity() { ExistingQuantity = 1000, Location = "A1" },
                new MaterialLocationQuantity() { ExistingQuantity = 2000, Location = "B2" },
                new MaterialLocationQuantity() { ExistingQuantity = 3000, Location = "C3" }
            };

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
            return Task.FromResult(true);
        }
    }
}
