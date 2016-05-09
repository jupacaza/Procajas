using Microsoft.WindowsAzure.Storage.Table;
using Procajas.Contracts;
using Procajas.Service.Models.TableEntities;
using System.Collections.Generic;
using System.Web.Http;

namespace Procajas.Service.Controllers
{
    //[Authorize]
    public class WarehouseController : BaseController
    {
        // GET warehouse/{department}/{id}
        public IHttpActionResult Get([FromUri]string department, [FromUri]string id)
        {
            // Retrieve a reference to the table.
            CloudTable table = this.serviceSettings.tableClient.GetTableReference(Constants.TableNames.WarehouseItems);

            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<WarehouseEntity>(department, id);

            // Execute the retrieve operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            // Print the phone number of the result.
            if (retrievedResult.Result == null)
            {
                return this.NotFound();
            }

            WarehouseEntity entity = (WarehouseEntity)retrievedResult.Result;
            WarehouseResource resource = new WarehouseResource()
            {
                Id = entity.Id,
                Material = entity.Material,
                Department = entity.Department,
                Quantity = entity.Quantity,
                DateOfInsertion = entity.DateOfInsertion,
                Location = entity.Location,
                InvoiceNumber = entity.InvoiceNumber
            };

            return this.Ok(resource);  
        }

        // GET warehouse/{department}
        public IEnumerable<WarehouseResource> Get([FromUri]string department)
        {
            // Retrieve a reference to the table.
            CloudTable table = this.serviceSettings.tableClient.GetTableReference(Constants.TableNames.WarehouseItems);

            // Construct the query operation for all customer entities where PartitionKey="{department}".
            TableQuery<WarehouseEntity> query = new TableQuery<WarehouseEntity>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, department));

            List<WarehouseResource> warehouseItemsByDepartment = new List<WarehouseResource>();
            foreach (WarehouseEntity entity in table.ExecuteQuery(query))
            {
                warehouseItemsByDepartment.Add(new WarehouseResource()
                {
                    Id = entity.Id,
                    Material = entity.Material,
                    Department = entity.Department,
                    Quantity = entity.Quantity,
                    DateOfInsertion = entity.DateOfInsertion,
                    Location = entity.Location,
                    InvoiceNumber = entity.InvoiceNumber
                });
            }

            return warehouseItemsByDepartment;
        }

        // POST warehouse
        public IHttpActionResult Post([FromBody] WarehouseResource resource)
        {
            WarehouseEntity entity = new WarehouseEntity()
            {
                Material = resource.Material,
                Department = resource.Department,
                Quantity = resource.Quantity,
                DateOfInsertion = resource.DateOfInsertion,
                Location = resource.Location,
                InvoiceNumber = resource.InvoiceNumber
            };

            // Retrieve a reference to the table.
            CloudTable table = this.serviceSettings.tableClient.GetTableReference(Constants.TableNames.WarehouseItems);

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

        // PUT warehouse/{id}
        public IHttpActionResult Put([FromUri]string id, [FromBody]WarehouseResource resource)
        {
            // Retrieve a reference to the table.
            CloudTable table = this.serviceSettings.tableClient.GetTableReference(Constants.TableNames.WarehouseItems);

            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<WarehouseEntity>(resource.Department, id);

            // Execute the operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            // Assign the result to a CustomerEntity object.
            WarehouseEntity updateEntity = (WarehouseEntity)retrievedResult.Result;

            if (updateEntity == null)
            {
                // The Warehouse item to be updated could not be found.
                return this.NotFound();
            }

            // Change the properties
            updateEntity.Material = resource.Material;
            updateEntity.Quantity = resource.Quantity;
            updateEntity.DateOfInsertion = resource.DateOfInsertion;
            updateEntity.Location = resource.Location;
            updateEntity.InvoiceNumber = resource.InvoiceNumber;

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