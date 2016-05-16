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

        // GET process/{department}/{id}
        public IHttpActionResult Get([FromUri]string department, [FromUri]string id)
        {
            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<ProcessEntity>(department, id);

            // Execute the retrieve operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            // Check if it was found
            if (retrievedResult.Result == null)
            {
                return this.NotFound();
            }

            ProcessEntity entity = (ProcessEntity)retrievedResult.Result;
            ProcessResource resource = new ProcessResource()
            {
                Id = entity.Id,
                Material = entity.Material,
                Department = entity.Department,
                Quantity = entity.Quantity,
                ProcessCheckinDate = entity.ProcessCheckinDate,
                Location = entity.Location,
                Details = entity.Details
            };

            return this.Ok(resource);
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

        // PUT process/{id}
        public IHttpActionResult Put([FromUri]string id, [FromBody]ProcessResource resource)
        {
            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<ProcessEntity>(resource.Department, id);

            // Execute the operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            // Assign the result to a CustomerEntity object.
            ProcessEntity updateEntity = (ProcessEntity)retrievedResult.Result;

            if (updateEntity == null)
            {
                // The Warehouse item to be updated could not be found.
                return this.NotFound();
            }

            // Change the properties
            updateEntity.Material = resource.Material;
            updateEntity.Quantity = resource.Quantity;
            updateEntity.ProcessCheckinDate = resource.ProcessCheckinDate;
            updateEntity.Location = resource.Location;
            updateEntity.Details = resource.Details;

            // Create the Replace TableOperation.
            TableOperation updateOperation = TableOperation.Replace(updateEntity);

            // Execute the operation.
            table.Execute(updateOperation);

            // Create the response to the client with the id of the row updated
            IdResource idResource = new IdResource() { Id = updateEntity.Id };

            return this.Ok(idResource);
        }
    }
}