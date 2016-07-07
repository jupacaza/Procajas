using Prism.Commands;
using Prism.Mvvm;
using Procajas.Models;
using Procajas.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Procajas.ViewModels
{
    public class WarehouseOutgoingViewModel : BindableBase
    {
        private DateTime checkoutDate = DateTime.Now;
        private string invoiceNumber;
        private string material;
        private List<MaterialLocationQuantity> quantitiesPerLocation;
        private string finishedProductLocation;

        private IProcajasStore store;

        public WarehouseOutgoingViewModel()
        {
            this.store = StoreFactory.Get(StoreTypes.ProcajasService);
            
            this.AcceptButtonCommand = new DelegateCommand(this.DoWarehouseCheckout);
        }

        #region public properties
        public DateTime CheckoutDate
        {
            get
            {
                return this.checkoutDate;
            }
            set
            {
                this.SetProperty(ref this.checkoutDate, value);
            }
        }

        public string InvoiceNumber
        {
            get
            {
                return this.invoiceNumber;
            }
            set
            {
                this.SetProperty(ref this.invoiceNumber, value);
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

        public string FinishedProductLocation
        {
            get
            {
                return this.finishedProductLocation;
            }
            set
            {
                this.SetProperty(ref this.finishedProductLocation, value);
            }
        }
        #endregion

        #region commands
        public ICommand AcceptButtonCommand { get; private set; }

        private async void DoWarehouseCheckout()
        {
            if (this.ValidateFields())
            {
                List<CheckoutWarehouseResource> checkoutResourceList = new List<CheckoutWarehouseResource>();
                int finalQuantity = 0;

                foreach(MaterialLocationQuantity mlc in this.quantitiesPerLocation)
                {
                    if (mlc.Selected == true && mlc.QuantityToUse > 0)
                    {
                        CheckoutWarehouseResource resource = new CheckoutWarehouseResource()
                        {
                            Id = mlc.Id,
                            Material = this.material,
                            Quantity = mlc.QuantityToUse,
                            Location = mlc.Location
                        };

                        finalQuantity += mlc.QuantityToUse;
                        checkoutResourceList.Add(resource);
                    }
                }

                await this.store.CheckoutFromWarehouse(checkoutResourceList);

                FinishedProductResource finishedProductResource = new FinishedProductResource()
                {
                    Material = this.material,
                    Quantity = finalQuantity,
                    FinishDate = this.checkoutDate,
                    InvoiceNumber = this.invoiceNumber,
                    Location = this.finishedProductLocation
                };

                await this.store.InsertFinishedProduct(finishedProductResource);
            }
        }

        private bool ValidateFields()
        {
            // TODO: Do validation
            return true;
        }

        private async void LoadQuantitiesPerLocation()
        {
            this.QuantitiesPerLocation = await this.store.GetQuantitiesPerLocationOfMaterial(this.material);
        }
        #endregion
    }
}
