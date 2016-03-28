using Procajas.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Procajas.Store
{
    public class ProcajasServiceStore : IProcajasStore
    {
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

        public Task<bool> InsertDiscrepanciesResources(List<DiscrepanciesResource> resourceList)
        {
            throw new NotImplementedException();
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
