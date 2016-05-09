using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Procajas.Service.Models.TableEntities
{
    public class WarehouseEntity : TableEntity
    {
        public WarehouseEntity()
        {
            this.RowKey = Guid.NewGuid().ToString();
        }

        public string Id
        {
            get
            {
                return this.RowKey;
            }
        }

        public string Material { get; set; }

        public string Department
        {
            get
            {
                return this.PartitionKey;
            }
            set
            {
                this.PartitionKey = value;
            }
        }

        public int Quantity { get; set; }

        public DateTime DateOfInsertion { get; set; }

        public string Location { get; set; }

        public string InvoiceNumber { get; set; }
    }
}