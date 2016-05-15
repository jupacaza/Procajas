using Prism.Commands;
using Prism.Mvvm;
using Procajas.Models;
using Procajas.Store;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Procajas.ViewModels
{
    public class ProcessCheckoutViewModel : BindableBase
    {
        private string selectedProcess;
        private List<string> processList;
        private string material;
        private DateTime checkoutDate = DateTime.Now;
        private List<ProcessCheckoutConsumedMaterial> consumedMaterials;
        private int quantityCreated;
        private string destinationLocation;

        private IProcajasStore store;

        public ProcessCheckoutViewModel()
        {
            this.store = StoreFactory.Get(StoreTypes.Test);
            this.AcceptButtonCommand = new DelegateCommand(this.DoProcessCheckout);
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
                this.LoadMaterialsInThisProcess();
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
                // TODO: MATERIALS SHOULD BE FETCHED FOM DB AND LISTED IN COMBOBOX OR AUTOCOMPLETE TEXTBOX
                return this.material;
            }
            set
            {
                this.SetProperty(ref this.material, value);

                // Validate that exit material starts with Process first 3 letters: IMP -> IMP_CajaX
                if (!Utilities.ValidateMaterialName(this.selectedProcess, this.material))
                {
                    throw new ApplicationException(Properties.Resources.materialProcessMismatchText);
                }
            }
        }

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

        public List<ProcessCheckoutConsumedMaterial> ConsumedMaterials
        {
            get
            {
                return this.consumedMaterials;
            }
            set
            {
                this.SetProperty(ref this.consumedMaterials, value);
            }
        }

        public string QuantityCreated
        {
            get
            {
                return this.quantityCreated.ToString();
            }
            set
            {
                int iValue;
                if (Utilities.ValidatePositiveInt(value, out iValue) && iValue > 0)
                {
                    this.SetProperty(ref this.quantityCreated, iValue);
                }
                else
                {
                    throw new ApplicationException("Not a valid positive greater than 0 integer value.");
                }
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

        private async void DoProcessCheckout()
        {
            if (this.ValidateFields())
            {
                // Checkout from process table
                // Insert to Discrepancies table
                List<CheckoutProcessResource> checkoutProcessResourceList = new List<CheckoutProcessResource>();
                List<DiscrepancyResource> discrepanciesResourceList = new List<DiscrepancyResource>();
                foreach (ProcessCheckoutConsumedMaterial consumedMaterial in this.consumedMaterials)
                {
                    if (consumedMaterial.Selected && 
                        (consumedMaterial.ConsumedInt + consumedMaterial.ReturnedInt + consumedMaterial.ScrappedInt > 0))
                    {
                        CheckoutProcessResource checkoutProcessResource = new CheckoutProcessResource()
                        {
                            Id = consumedMaterial.Id,
                            Material = consumedMaterial.Material,
                            Quantity = consumedMaterial.TotalQuantity,
                            Location = this.selectedProcess
                        };

                        checkoutProcessResourceList.Add(checkoutProcessResource);

                        if (consumedMaterial.ScrappedInt > 0)
                        {
                            DiscrepancyResource discrepanciesResource = new DiscrepancyResource()
                            {
                                Department = this.selectedProcess,
                                DiscrepancyDate = this.checkoutDate,
                                Material = consumedMaterial.Material,
                                Job = this.material,
                                Quantity = consumedMaterial.ScrappedInt
                            };

                            discrepanciesResourceList.Add(discrepanciesResource);
                        }
                    }
                }

                await this.store.CheckoutFromProcess(checkoutProcessResourceList);
                await this.store.InsertDiscrepancyResources(discrepanciesResourceList);

                // Insert to Warehouse table
                WarehouseResource warehouseResource = new WarehouseResource()
                {
                    Material = this.material,
                    Quantity = this.quantityCreated,
                    DateOfInsertion = this.checkoutDate,
                    Department = this.selectedProcess,
                    Location = this.destinationLocation
                };

                await this.store.InsertWarehouseResource(warehouseResource);                
            }
        }
        #endregion

        #region private methods
        private bool ValidateFields()
        {
            // TODO: Validate
            // Validate selected consumed materials are valid
            foreach (ProcessCheckoutConsumedMaterial consumedMaterial in this.consumedMaterials)
            {
                if (consumedMaterial.Selected && !consumedMaterial.IsValid)
                {
                    return false;
                }
            }

            if (!Utilities.ValidateMaterialName(this.selectedProcess, this.material))
            {
                return false;
            }

            return true;
        }

        private async void LoadMaterialsInThisProcess()
        {
            this.ConsumedMaterials = await this.store.GetMaterialsInProcess(this.selectedProcess);
        }

        private async void LoadProcessList()
        {
            this.processList = await this.store.GetAdminItemsByType(AdminItemTypes.Process);
        }
        #endregion
    }
}
