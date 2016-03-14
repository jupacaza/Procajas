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
        private DateTime checkinDate = DateTime.Now;
        private List<MaterialLocationQuantity> quantitiesPerLocation;
        private string destinationLocation;

        private IProcajasStore store;
        
        public ProcessCheckinViewModel()
        {
            this.store = StoreFactory.Get(StoreTypes.Test);
            this.processList = Constants.ProcessList;
            this.AcceptButtonCommand = new DelegateCommand(this.DoProcessCheckin);
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
            }
        }

        public IList<string> ProcessList
        {
            get { return this.processList; }
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
                List<CheckoutResource> checkoutResourceList = new List<CheckoutResource>();
                foreach (MaterialLocationQuantity mlq in this.quantitiesPerLocation)
                {
                    if (mlq.Selected && mlq.QuantityToUse > 0)
                    {
                        CheckoutResource checkoutResource = new CheckoutResource()
                        {
                            Material = this.material,
                            Location = mlq.Location,
                            Quantity = mlq.QuantityToUse
                        };

                        checkoutResourceList.Add(checkoutResource);
                    }
                }

                await store.CheckoutWarehouseResource(checkoutResourceList);

                // Insert new record to process table
                ProcessResource processResource = new ProcessResource()
                {
                    ProcessCheckinDate = this.checkinDate,
                    Material = this.material,
                    Department = this.selectedProcess,
                    Location = this.destinationLocation
                };

                await store.InsertProcessResource(processResource);
            }
            else
            {
                // show a message box or something
            }
        }

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
            QuantitiesPerLocationResource qplResource = new QuantitiesPerLocationResource()
            {
                Material = this.material
            };

            this.QuantitiesPerLocation = await this.store.GetQuantitiesPerLocation(qplResource);
        }
        #endregion
    }
}
