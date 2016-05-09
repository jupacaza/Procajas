using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Procajas.Service.Settings
{
    public class ServiceSettings
    {
        private static ServiceSettings serviceSettings;

        private ServiceSettings() {}

        private CloudStorageAccount storageAccount { get; set; }

        public CloudTableClient tableClient { get; set; }

        public static ServiceSettings Instance
        {
            get
            {
                if (serviceSettings == null)
                {
                    // Get the storage account
                    CloudStorageAccount newStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

                    // Create the table client.
                    CloudTableClient newTableClient = newStorageAccount.CreateCloudTableClient();

                    ServiceSettings settings = new ServiceSettings()
                    {
                        storageAccount = newStorageAccount,
                        tableClient = newTableClient
                    };

                    serviceSettings = settings;
                }

                return serviceSettings;
            }
        }
    }
}