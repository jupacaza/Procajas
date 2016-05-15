using Microsoft.WindowsAzure.Storage.Table;
using Procajas.Contracts;
using Procajas.Service.Models.TableEntities;
using System.Collections.Generic;
using System.Web.Http;

namespace Procajas.Service.Controllers
{
    public class ProcessController : BaseController
    {
        private CloudTable table;

        public ProcessController() : base()
        {
            if (table == null)
            {
                // Retrieve a reference to the table.
                table = this.serviceSettings.tableClient.GetTableReference(Constants.TableNames.ProcessItems);
            }
        }

        // GET process/{department}
        public IEnumerable<ProcessResource> Get([FromUri]string department)
        {
            // Construct the query operation for all customer entities where PartitionKey={partitionKey}.
            TableQuery<ProcessEntity> query = new TableQuery<ProcessEntity>().Where(
                TableQuery.GenerateFilterCondition(Constants.PartitionKey, QueryComparisons.Equal, department));

            List<ProcessResource> itemsByDepartment = new List<ProcessResource>();
            foreach (ProcessEntity entity in table.ExecuteQuery(query))
            {
                // return only entities with quantity > 0
                if (entity.Quantity > 0)
                {
                    itemsByDepartment.Add(new ProcessResource()
                    {
                        Id = entity.Id,
                        Material = entity.Material,
                        Department = entity.Department,
                        Quantity = entity.Quantity,
                        ProcessCheckinDate = entity.ProcessCheckinDate,
                        Location = entity.Location,
                        Details = entity.Details
                    });
                }
            }

            return itemsByDepartment;
        }

        // POST process
        public IHttpActionResult Post([FromBody] ProcessResource resource)
        {
            ProcessEntity entity = new ProcessEntity()
            {
                Material = resource.Material,
                Department = resource.Department,
                Quantity = resource.Quantity,
                ProcessCheckinDate = resource.ProcessCheckinDate,
                Location = resource.Location,
                Details = resource.Details
            };

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(entity);

            // Execute the insert operation.
            table.Execute(insertOperation);

            // Create the response to the client with the id of the row created
            IdResource idResource = new IdResource() { Id = entity.Id };

            return this.Ok(idResource);
        }
    }
}