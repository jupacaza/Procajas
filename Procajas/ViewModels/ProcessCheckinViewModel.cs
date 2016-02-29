using Prism.Commands;
using Prism.Mvvm;
using Procajas.Models;
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
        
        public ProcessCheckinViewModel()
        {
            // TODO: load all non-static data from DB
            this.processList = new List<string>
            {
                "IMP",
                "SUA",
                "XYZ"
            };

            this.quantitiesPerLocation = new List<MaterialLocationQuantity>
            {
                new MaterialLocationQuantity { Location = "A1", ExistingQuantity = 3000 },
                new MaterialLocationQuantity { Location = "B2", ExistingQuantity = 2000 },
                new MaterialLocationQuantity { Location = "C3", ExistingQuantity = 1000 }
            };
            ////

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
        
        private void DoProcessCheckin()
        {
        }
        #endregion
    }
}
