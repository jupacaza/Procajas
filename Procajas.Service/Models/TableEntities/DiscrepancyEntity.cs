using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Procajas.Service.Models.TableEntities
{
    public class DiscrepancyEntity : TableEntity
    {
        public DiscrepancyEntity()
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

        public string Material
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

        public string Department { get; set; }

        public int Quantity { get; set; }

        public DateTime DiscrepancyDate { get; set; }

        public string Job { get; set; }
    }
}