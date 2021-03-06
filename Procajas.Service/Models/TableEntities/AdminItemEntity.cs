﻿using Microsoft.WindowsAzure.Storage.Table;

namespace Procajas.Service.Models.TableEntities
{
    public class AdminItemEntity : TableEntity
    {
        public AdminItemEntity()
        {
        }

        public AdminItemEntity(string name, string type)
        {
            this.PartitionKey = type;
            this.RowKey = name;
        }
                
        public string Name
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
        
        public string Type
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