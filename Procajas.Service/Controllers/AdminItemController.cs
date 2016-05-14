using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Procajas.Contracts;
using Procajas.Service.Models.TableEntities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Procajas.Service.Controllers
{
    //[Authorize]
    public class AdminItemController : BaseController
    {
        private CloudTable table;

        public AdminItemController() : base()
        {
            if (table == null)
            {
                // Retrieve a reference to the table.
                table = this.serviceSettings.tableClient.GetTableReference(Constants.TableNames.AdminItems);
            }
        }

        // GET adminItem/[material|process|location]
        public IEnumerable<AdminItemResource> Get([FromUri]string type)
        {
            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<AdminItemEntity> query = new TableQuery<AdminItemEntity>().Where(
                TableQuery.GenerateFilterCondition(Constants.PartitionKey, QueryComparisons.Equal, type));

            List<AdminItemResource> adminItemsByType = new List<AdminItemResource>();
            foreach (AdminItemEntity entity in table.ExecuteQuery(query))
            {
                adminItemsByType.Add(new AdminItemResource() { Name = entity.Name, Type = entity.Type });
            }

            return adminItemsByType;
        }

        // POST adminItem
        public IHttpActionResult Post([FromBody]AdminItemResource resource)
        {
            AdminItemEntity entity = new AdminItemEntity(resource.Name, resource.Type);

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(entity);

            // Execute the insert operation.
            table.Execute(insertOperation);

            return this.Ok();
        }

        // DELETE adminItem/{type}/{name}
        public IHttpActionResult Delete([FromUri]string type, [FromUri]string name)
        {
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
                return this.BadRequest("Delete operation failed because the item to be deleted does not exist.");
            }
        }
    }
}
