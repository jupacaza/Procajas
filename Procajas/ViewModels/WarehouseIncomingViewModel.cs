using Prism.Commands;
using Prism.Mvvm;
using Procajas.Models;
using Procajas.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Procajas.ViewModels
{
    public class WarehouseIncomingViewModel : BindableBase
    {
        private string material;
        private int quantity = 1;
        private string location;
        private DateTime checkinDate = DateTime.Now;
        private string invoiceNumber;
        private List<string> materialList;
        private List<string> locationList;

        private IProcajasStore store;

        public WarehouseIncomingViewModel()
        {
            this.store = StoreFactory.Get(StoreTypes.Test);
            this.AcceptButtonCommand = new DelegateCommand(this.DoWarehouseCheckin);
            this.LoadMaterialList();
            this.LoadLocationList();
        }

        #region public properties
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

        public string Quantity
        {
            get
            {
                return this.quantity.ToString();
            }
            set
            {
                int iValue;
                if(!Utilities.ValidatePositiveInt(value, out iValue) || iValue <= 0)
                {
                    throw new ApplicationException("Input has to be a valid numerical positive value");
                }

                this.SetProperty(ref this.quantity, iValue);
            }
        }

        public string Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.SetProperty(ref this.location, value);
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

        public List<string> LocationList
        {
            get
            {
                return this.locationList;
            }
        }
        #endregion

        #region commands
        public ICommand AcceptButtonCommand { get; private set; }

        private async void DoWarehouseCheckin()
        {
            if (this.ValidateFields())
            {
                // Insert new record to warehouse table
                WarehouseResource warehouseResource = new WarehouseResource()
                {
                    Material = this.material,
                    Department = Utilities.GetDepartmentFromMaterial(this.material),
                    Quantity = this.quantity,
                    DateOfInsertion = this.checkinDate,
                    Location = this.location,
                    InvoiceNumber = this.invoiceNumber
                };

                await this.store.InsertWarehouseResource(warehouseResource);
            }
        }

        private bool ValidateFields()
        {
            // TODO: Validate
            return true;
        }

        private async void LoadMaterialList()
        {
            // pass a filter to only pick materials that belong to the PRI process
            Dictionary<bool, string> filter = new Dictionary<bool, string>()
            {
                { true, "PRI" }
            };

            this.MaterialList = await this.store.GetAdminItemsByType(AdminItemTypes.Material, filter);
        }

        private async void LoadLocationList()
        {
            this.locationList = await this.store.GetAdminItemsByType(AdminItemTypes.Location);
        }
        #endregion
    }
}
