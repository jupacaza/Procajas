using Prism.Commands;
using Prism.Mvvm;
using Procajas.Models;
using Procajas.Store;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Procajas.ViewModels
{
    public class AdministratorViewModel : BindableBase
    {
        private List<TypeValueDescription> adminOperations;
        private List<TypeValueDescription> itemTypes;
        private TypeValueDescription selectedOperation;
        private TypeValueDescription selectedItemType;
        private string itemName;

        private IProcajasStore store;

        public AdministratorViewModel()
        {
            this.store = StoreFactory.Get(StoreTypes.Test);
            this.adminOperations = Constants.AdminOperationList;
            this.itemTypes = Constants.AdminItemTypeList;
            this.AcceptButtonCommand = new DelegateCommand(this.DoAdministratorOperation);
        }

        #region public properties
        public TypeValueDescription SelectedOperation
        {
            get
            {
                return this.selectedOperation;
            }
            set
            {
                this.SetProperty(ref this.selectedOperation, value);
            }
        }

        public TypeValueDescription SelectedItemType
        {
            get
            {
                return this.selectedItemType;
            }
            set
            {
                this.SetProperty(ref this.selectedItemType, value);
            }
        }

        public IList<TypeValueDescription> AdminOperations
        {
            get { return this.adminOperations; }
        }

        public IList<TypeValueDescription> ItemTypes
        {
            get { return this.itemTypes; }
        }

        public string ItemName
        {
            get
            {
                return this.itemName;
            }
            set
            {
                this.SetProperty(ref this.itemName, value);
                if (!this.ValidateFields())
                {
                    throw new ApplicationException("One or more fields in the Administrator form is invalid.");
                }
            }
        }
        #endregion

        #region commands
        public ICommand AcceptButtonCommand { get; private set; }

        private async void DoAdministratorOperation()
        {
            if (this.ValidateFields())
            {
                AdminOperations operation = (AdminOperations)this.selectedOperation.Value;
                AdminItemTypes itemType = (AdminItemTypes)this.selectedItemType.Value;
                switch (operation)
                {
                    case Models.AdminOperations.Create:
                        await this.store.InsertAdminItemByType(this.itemName, itemType);
                        break;
                    case Models.AdminOperations.Delete:
                        await this.store.DeleteAdminItemByType(this.itemName, itemType);
                        break;
                }
            }
        }
        #endregion

        #region private methods
        private bool ValidateFields()
        {
            if (this.selectedOperation == null || this.selectedItemType == null
                || this.selectedOperation.Value == null || this.selectedItemType.Value == null)
            {
                return false;
            }

            if (!(this.selectedOperation.Value is AdminOperations) || !(this.selectedItemType.Value is AdminItemTypes))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.itemName))
            {
                return false;
            }
            
            if (this.selectedItemType.Value is AdminItemTypes)
            {
                switch ((AdminItemTypes)this.selectedItemType.Value)
                {
                    case AdminItemTypes.Process:
                        if (!Utilities.ValidateProcessNameFormat(this.itemName))
                        {
                            return false;
                        }
                        break;
                    case AdminItemTypes.Material:
                        if (!Utilities.ValidateMaterialNameFormat(this.itemName))
                        {
                            return false;
                        }
                        break;
                }
            }

            return true;
        }
        #endregion
    }
}
