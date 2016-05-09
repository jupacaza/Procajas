namespace Procajas.Service
{
    public class Constants
    {
        public const string StorageConnectionString = "StorageConnectionString";
        public const string PartitionKey = "PartitionKey";
        public const string RowKey = "RowKey";

        public class TableNames
        {
            public const string AdminItems = "adminItems";
            public const string DiscrepancyItems = "discrepancyItems";
            public const string FinishedProductItems = "finishedProductItems";
            public const string ProcessItems = "processItems";
            public const string WarehouseItems = "warehouseItems";
        }
    }
}