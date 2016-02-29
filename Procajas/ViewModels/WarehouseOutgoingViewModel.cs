using Prism.Commands;
using Prism.Mvvm;
using Procajas.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Procajas.ViewModels
{
    public class WarehouseOutgoingViewModel : BindableBase
    {
        private DateTime checkoutDate = DateTime.Now;
        private string invoiceNumber;
        private string material;
        private List<MaterialLocationQuantity> quantitiesPerLocation;

        public WarehouseOutgoingViewModel()
        {
            // TODO: load all non-static data from DB
            this.material = "CAJ_PinkZapatilla";

            this.quantitiesPerLocation = new List<MaterialLocationQuantity>
            {
                new MaterialLocationQuantity { Location = "A1", ExistingQuantity = 3000 },
                new MaterialLocationQuantity { Location = "B2", ExistingQuantity = 2000 },
                new MaterialLocationQuantity { Location = "C3", ExistingQuantity = 1000 }
            };
            ////
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
        #endregion

        #region commands
        public ICommand AcceptButtonCommand { get; private set; }

        private void DoWarehouseCheckout()
        {
        }
        #endregion
    }
}
