using Prism.Mvvm;
using Procajas.Properties;
using System;

namespace Procajas.Models
{
    public class ProcessCheckoutConsumedMaterial : BindableBase
    {
        private bool selected;
        private string material;
        private int existing;
        private int consumed;
        private int scrapped;
        private int returned;
        private bool isValid = true;

        public string Id { get; set; }

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

        public int Existing
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
                this.SetProperty(ref this.consumed, this.ThrowIfNotPositiveInt(value));
                this.Validate();
            }
        }

        public int ConsumedInt
        {
            get
            {
                return this.consumed;
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
                this.SetProperty(ref this.scrapped, this.ThrowIfNotPositiveInt(value));
                this.Validate();
            }
        }

        public int ScrappedInt
        {
            get
            {
                return this.scrapped;
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
                this.SetProperty(ref this.returned, this.ThrowIfNotPositiveInt(value));
                this.Validate();
            }
        }

        public int ReturnedInt
        {
            get
            {
                return this.returned;
            }
        }

        public int TotalQuantity
        {
            get
            {
                return this.consumed + this.returned + this.scrapped;
            }
        }

        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            private set
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

        private int ThrowIfNotPositiveInt(string sValue)
        {
            int iValue;
            if (!Utilities.ValidatePositiveInt(sValue, out iValue))
            {
                throw new ApplicationException("Input has to be a valid integer positive value");
            }

            return iValue;
        }
    }
}
