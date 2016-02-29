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


        public WarehouseOutgoingViewModel()
        {
            this.AcceptButtonCommand = new DelegateCommand(this.DoWarehouseCheckout);
        }

        #region public properties
        #endregion

        #region commands
        public ICommand AcceptButtonCommand { get; private set; }

        private void DoWarehouseCheckout()
        {
        }
        #endregion
    }
}
