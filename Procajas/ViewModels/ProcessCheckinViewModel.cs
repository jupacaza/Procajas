using Prism.Commands;
using Prism.Mvvm;
using Procajas.Models;
using Procajas.Store;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Procajas.ViewModels
{
    public class ProcessCheckinViewModel : BindableBase
    {
        private string selectedProcess;
        private List<string> processList;
        private string material;
        private List<string> materialList;
        private DateTime checkinDate = DateTime.Now;
        private List<MaterialLocationQuantity> quantitiesPerLocation;
        private string destinationLocation;

        private IProcajasStore store;
        
        public ProcessCheckinViewModel()
        {
            this.store = StoreFactory.Get(StoreTypes.Test);
            this.AcceptButtonCommand = new DelegateCommand(this.DoProcessCheckin);
            this.LoadProcessList();
        }

        #region public properties
        public string SelectedProcess
        {
            get
            {
                return this.selectedProcess;
            }
            set
            {
                this.SetProperty(ref this.selectedProcess, value);
                this.LoadMaterialList();
            }
        }

        public List<string> ProcessList
        {
            get
            {
                return this.processList;
            }
            private set
            {
                this.SetProperty(ref this.processList, value);
            }
        }

        public string Material
        {
            get
            {
                return this.material;
            }
            set
            {
                this.SetProperty(ref this.material, value);
                this.LoadQuantitiesPerLocation();
            }
        }

        public List<string> MaterialList
        {
            get
            {
                return this.materialList;
            }
            private set
            {
                this.SetProperty(ref this.materialList, value);
            }
        }

        public DateTime CheckinDate
        {
            get
            {
                return this.checkinDate;
            }
            set
            {
                this.SetProperty(ref this.checkinDate, value);
            }
        }

        public List<MaterialLocationQuantity> QuantitiesPerLocation
        {
            get
            {
                return this.quantitiesPerLocation;
            }
            set
            {
                this.SetProperty(ref this.quantitiesPerLocation, value);
            }
        }

        public string DestinationLocation
        {
            get
            {
                return this.destinationLocation;
            }
            set
            {
                this.SetProperty(ref this.destinationLocation, value);
            }
        }
        #endregion

        #region commands
        public ICommand AcceptButtonCommand { get; private set; }
        
        private async void DoProcessCheckin()
        {
            if (this.ValidateFields())
            {
                // Checkout from warehouse table
                int totalQuantityToUse = 0;
                List<CheckoutWarehouseResource> checkoutResourceList = new List<CheckoutWarehouseResource>();
                foreach (MaterialLocationQuantity mlq in this.quantitiesPerLocation)
                {
                    if (mlq.Selected && mlq.QuantityToUse > 0)
                    {
                        CheckoutWarehouseResource checkoutResource = new CheckoutWarehouseResource()
                        {
                            Id = mlq.Id,
                            Material = this.material,
                            Location = mlq.Location,
                            Quantity = mlq.QuantityToUse
                        };

                        checkoutResourceList.Add(checkoutResource);
                        totalQuantityToUse += mlq.QuantityToUse;
                    }
                }

                await store.CheckoutFromWarehouse(checkoutResourceList);

                // Insert new record to process table
                ProcessResource processResource = new ProcessResource()
                {
                    ProcessCheckinDate = this.checkinDate,
                    Material = this.material,
                    Department = this.selectedProcess,
                    Quantity = totalQuantityToUse,
                    Location = this.destinationLocation
                };

                await store.InsertProcessResource(processResource);
            }
            else
            {
                // show a message box or something
            }
        }
        #endregion

        #region private methods
        private bool ValidateFields()
        {
            // TODO: Do validation

            // Validate quantities
            foreach (MaterialLocationQuantity mlq in this.quantitiesPerLocation)
            {
                if (mlq.Selected && mlq.QuantityToUse > mlq.ExistingQuantity)
                {
                    return false;
                }
            }

            return true;
        }

        private async void LoadQuantitiesPerLocation()
        {
            this.QuantitiesPerLocation = await this.store.GetQuantitiesPerLocationOfMaterial(this.material);
        }

        private async void LoadProcessList()
        {
            this.ProcessList = await this.store.GetAdminItemsByType(AdminItemTypes.Process);
        }

        private async void LoadMaterialList()
        {
            // pass a filter to only pick materials that belong to the "XXX" process
            Dictionary<string, bool> filter = new Dictionary<string, bool>()
            {
                { this.selectedProcess, true }
            };

            this.MaterialList = await this.store.GetAdminItemsByType(AdminItemTypes.Material, filter);
        }
        #endregion
    }
}
