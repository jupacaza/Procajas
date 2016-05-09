using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Procajas.Service.Models.TableEntities
{
    public class FinishedProductEntity : TableEntity
    {
        public FinishedProductEntity()
        {
        }

        public string Material
        {
            get
            {
                return this.RowKey;
            }
            set
            {
                this.RowKey = value;
            }
        }

        public int Quantity { get; set; }

        public DateTime FinishDate { get; set; }

        public string Location { get; set; }

        public string InvoiceNumber
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
    }
}