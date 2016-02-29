using Prism.Commands;
using Prism.Mvvm;
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
        private double quantity = 1;
        private string location;
        private DateTime checkinDate = DateTime.Now;
        private string invoiceNumber;

        public WarehouseIncomingViewModel()
        {

            this.AcceptButtonCommand = new DelegateCommand(this.DoWarehouseCheckin);
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
                double dValue;
                if(!Utilities.ValidatePositiveDouble(value, out dValue) || dValue <= 0)
                {
                    throw new ApplicationException("Input has to be a valid numerical positive value");
                }

                this.SetProperty(ref this.quantity, dValue);
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
        #endregion

        #region commands
        public ICommand AcceptButtonCommand { get; private set; }

        private void DoWarehouseCheckin()
        {
        }
        #endregion
    }
}
