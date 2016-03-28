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

        private List<string> Materials = new List<string>()
        {
            "IMP_AdidasSoccer",
            "SUA_EleganteCafe",
            "COR_DeLeon",
            "PRI_MateriaPrima1",
            "CAJ_Elefante"
        };

        private List<string> Locations = new List<string>()
        {
            "A1",
            "B2",
            "C3"
        };

        public Task<bool> CheckoutProcessResource(List<CheckoutProcessResource> resourceList)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CheckoutWarehouseResource(List<CheckoutResource> resourceList)
        {
            return Task.FromResult(true);
        }
        
        public Task<List<string>> GetAdminItemsByType(AdminItemTypes adminItemType, IDictionary<bool, string> filter = null)
        {
            switch (adminItemType)
            {
                case AdminItemTypes.Material:
                    if (filter != null)
                    {
                        // filter is only used in materials.
                        // go through the materials and pick only the ones where:
                        // 1. the key of the filter says true and the material name starts with the value of the filter
                        // 2. the key of the filter says false and the material does not start with the value of the filter
                        return Task.FromResult(this.Materials.Where(
                            s =>
                            {
                                // go through each value of the filter
                                foreach (KeyValuePair<bool, string> kvp in filter)
                                {
                                    // XOR will give true when the values are different. We know it's a match when the boolean values are the same.
                                    if (kvp.Value != null && !(kvp.Key ^ s.StartsWith(kvp.Value, StringComparison.InvariantCultureIgnoreCase)))
                                    {
                                        return true;
                                    }
                                }

                                return false;
                            }
                        ).ToList());
                    }
                    else
                    {
                        return Task.FromResult(this.Materials);
                    }
                    
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
