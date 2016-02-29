using Prism.Commands;
using Prism.Mvvm;
using Procajas.Models;
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
        private string destinationLocation;

        public ProcessCheckoutViewModel()
        {
            // TODO: load all non-static data from DB
            this.processList = new List<string>
            {
                "IMP",
                "SUA",
                "XYZ"
            };

            this.consumedMaterials = new List<ProcessCheckoutConsumedMaterial>
            {
                new ProcessCheckoutConsumedMaterial { Material = "Agua", Existing = 3000 },
                new ProcessCheckoutConsumedMaterial { Material = "Aire", Existing = 2000 },
                new ProcessCheckoutConsumedMaterial { Material = "Fuego", Existing = 1000 }
            };
            ////

            this.AcceptButtonCommand = new DelegateCommand(this.DoProcessCheckout);
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

        private void DoProcessCheckout()
        {
        }
        #endregion
    }
}
