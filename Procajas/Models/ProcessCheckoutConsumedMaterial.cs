using Prism.Mvvm;
using Procajas.Properties;
using System;

namespace Procajas.Models
{
    public class ProcessCheckoutConsumedMaterial : BindableBase
    {
        private bool selected;
        private string material;
        private double existing;
        private double consumed;
        private double scrapped;
        private double returned;
        private bool isValid = true;

        public bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                this.Consumed = value ? this.Existing.ToString() : "0";
                if (!value)
                {
                    this.Scrapped = "0";
                    this.Returned = "0";
                }

                this.SetProperty(ref this.selected, value);
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

        public double Existing
        {
            get
            {
                return this.existing;
            }
            set
            {
                this.SetProperty(ref this.existing, value);
                this.Validate();
            }
        }

        public string Consumed
        {
            get
            {
                return this.consumed.ToString();
            }
            set
            {
                this.SetProperty(ref this.consumed, this.ThrowIfNotPositiveDouble(value));
                this.Validate();
            }
        }

        public string Scrapped
        {
            get
            {
                return this.scrapped.ToString();
            }
            set
            {
                this.SetProperty(ref this.scrapped, this.ThrowIfNotPositiveDouble(value));
                this.Validate();
            }
        }

        public string Returned
        {
            get
            {
                return this.returned.ToString();
            }
            set
            {
                this.SetProperty(ref this.returned, this.ThrowIfNotPositiveDouble(value));
                this.Validate();
            }
        }

        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.SetProperty(ref this.isValid, value);
            }
        }

        private void Validate()
        {
            if (!this.ValidateMaterialCount())
            {
                this.IsValid = false;
            }
            else
            {
                this.IsValid = true;
            }
        }

        private bool ValidateMaterialCount()
        {
            return this.selected ? this.existing - this.consumed - this.scrapped - this.returned == 0 : true;
        }

        private double ThrowIfNotPositiveDouble(string sValue)
        {
            double dValue;
            if (!Utilities.ValidatePositiveDouble(sValue, out dValue))
            {
                throw new ApplicationException("Input has to be a valid numerical positive value");
            }

            return dValue;
        }
    }
}
