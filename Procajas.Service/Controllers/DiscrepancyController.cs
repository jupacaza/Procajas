using Microsoft.WindowsAzure.Storage.Table;
using Procajas.Contracts;
using Procajas.Service.Models.TableEntities;
using System.Collections.Generic;
using System.Web.Http;

namespace Procajas.Service.Controllers
{
    [Authorize]
    public class DiscrepancyController : BaseController
    {
        private CloudTable table;

        public DiscrepancyController() : base()
        {
            if (table == null)
            {
                // Retrieve a reference to the table.
                table = this.serviceSettings.tableClient.GetTableReference(Constants.TableNames.DiscrepancyItems);
            }
        }

        // GET discrepancy/{material}/{id}
        public IHttpActionResult Get([FromUri]string material, [FromUri]string id)
        {
            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<DiscrepancyEntity>(material, id);

            // Execute the retrieve operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            // Check if it was found
            if (retrievedResult.Result == null)
            {
                return this.NotFound();
            }

            DiscrepancyEntity entity = (DiscrepancyEntity)retrievedResult.Result;
            DiscrepancyResource resource = new DiscrepancyResource()
            {
                Id = entity.Id,
                Material = entity.Material,
                Department = entity.Department,
                Quantity = entity.Quantity,
                DiscrepancyDate = entity.DiscrepancyDate,
                Job = entity.Job
            };

            return this.Ok(resource);
        }

        // GET discrepancy/{material}
        public IEnumerable<DiscrepancyResource> Get([FromUri]string material)
        {
            // Construct the query operation for all customer entities where PartitionKey={partitionKey}.
            TableQuery<DiscrepancyEntity> query = new TableQuery<DiscrepancyEntity>().Where(
                TableQuery.GenerateFilterCondition(Constants.PartitionKey, QueryComparisons.Equal, material));

            List<DiscrepancyResource> itemsByDepartment = new List<DiscrepancyResource>();
            foreach (DiscrepancyEntity entity in table.ExecuteQuery(query))
            {
                itemsByDepartment.Add(new DiscrepancyResource()
                {
                    Id = entity.Id,
                    Material = entity.Material,
                    Department = entity.Department,
                    Quantity = entity.Quantity,
                    DiscrepancyDate = entity.DiscrepancyDate,
                    Job = entity.Job
                });
            }

            return itemsByDepartment;
        }

        // POST discrepancy
        public IHttpActionResult Post([FromBody] IEnumerable<DiscrepancyResource> resources)
        {
            // Create the batch operation.
            TableBatchOperation batchOperation = new TableBatchOperation();
            List<string> ids = new List<string>();
            foreach (DiscrepancyResource resource in resources)
            {
                DiscrepancyEntity entity = new DiscrepancyEntity()
                {
                    Material = resource.Material,
                    Department = resource.Department,
                    Quantity = resource.Quantity,
                    DiscrepancyDate = resource.DiscrepancyDate,
                    Job = resource.Job
                };

                batchOperation.Insert(entity);
                ids.Add(entity.Id);
            }

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();
            
            // Execute the batch operation.
            table.ExecuteBatch(batchOperation);

            // Create the response to the client with the ids of the rows created
            return this.Ok(ids);            
        }
    }
}