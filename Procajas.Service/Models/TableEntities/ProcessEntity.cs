using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

namespace Procajas.Service.Models.TableEntities
{
    public class ProcessEntity : TableEntity
    {
        public ProcessEntity()
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

        public DateTime ProcessCheckinDate { get; set; }

        public string Location { get; set; }

        public IDictionary<object, object> Details { get; set; }
    }
}