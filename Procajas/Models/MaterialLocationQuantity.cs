using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procajas.Models
{
    public class MaterialLocationQuantity : BindableBase
    {
        private bool selected;
        private string location;
        private int existingQuantity;
        private int quantityToUse;

        public bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                this.QuantityToUse = value == true ? this.ExistingQuantity : 0;
                this.SetProperty(ref selected, value);
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
                this.SetProperty(ref location, value);
            }
        }

        public int ExistingQuantity
        {
            get
            {
                return this.existingQuantity;
            }
            set
            {
                this.SetProperty(ref existingQuantity, value);
            }
        }

        public int QuantityToUse
        {
            get
            {
                return this.quantityToUse;
            }
            set
            {
                this.SetProperty(ref quantityToUse, value);
                if (value > this.existingQuantity)
                {
                    throw new ApplicationException("Quantity to use cannot be greater than existing quantity.");
                }
            }
        }
    }
}
