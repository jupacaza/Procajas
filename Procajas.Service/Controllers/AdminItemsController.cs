using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Procajas.Contracts;
using Procajas.Service.Models.TableEntities;
using System.Collections.Generic;
using System.Web.Http;

namespace Procajas.Service.Controllers
{
    //[Authorize]
    public class AdminItemsController : ApiController
    {
        // TODO: put these in a settings class
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        const string AdminItemsTableName = "adminItems";

        public AdminItemsController()
        {
            // Get the storage account
            storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageEmulatorConnectionString"));

            // Create the table client.
            tableClient = storageAccount.CreateCloudTableClient();
        }

        // GET adminItems/[material|process|location]
        public string Get(string type)
        {
            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("adminItems");

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<AdminItemEntity> query = new TableQuery<AdminItemEntity>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, type));

            List<AdminItemResource> adminItemsByType = new List<AdminItemResource>();
            foreach (AdminItemEntity entity in table.ExecuteQuery(query))
            {
                adminItemsByType.Add(new AdminItemResource() { Name = entity.Name, Type = entity.Type });
            }

            return JsonConvert.SerializeObject(adminItemsByType);
        }

        // POST adminItems
        public IHttpActionResult Post([FromBody]string adminItemResource)
        {
            AdminItemResource resource;
            try
            {
                resource = JsonConvert.DeserializeObject<AdminItemResource>(adminItemResource);
            }
            catch (JsonSerializationException)
            {
                return this.BadRequest("The request body could not be deserialized into an AdminItem.");
            }
            
            AdminItemEntity entity = new AdminItemEntity(resource.Name, resource.Type);

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference(AdminItemsTableName);

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(entity);

            // Execute the insert operation.
            table.Execute(insertOperation);

            return this.Ok();
        }

        // DELETE adminItems/<type>/<name>
        public IHttpActionResult Delete(string type, string name)
        {
            // Create the CloudTable that represents the "people" table.
            CloudTable table = tableClient.GetTableReference(AdminItemsTableName);

            // Create a retrieve operation that expects a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<AdminItemEntity>(type, name);

            // Execute the operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            // Assign the result to a CustomerEntity.
            AdminItemEntity deleteEntity = (AdminItemEntity)retrievedResult.Result;

            // Create the Delete TableOperation.
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                // Execute the operation.
                table.Execute(deleteOperation);

                return this.Ok();
            }
            else
            {
                return this.BadRequest("Delete operation failed because of wrong parameters.");
            }
        }
    }
}
