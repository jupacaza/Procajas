using Microsoft.WindowsAzure.Storage.Table;
using Procajas.Contracts;
using Procajas.Service.Models.TableEntities;
using System.Collections.Generic;
using System.Web.Http;

namespace Procajas.Service.Controllers
{
    [Authorize]
    public class FinishedProductController : BaseController
    {
        private CloudTable table;

        public FinishedProductController() : base()
        {
            if (table == null)
            {
                // Retrieve a reference to the table.
                table = this.serviceSettings.tableClient.GetTableReference(Constants.TableNames.FinishedProductItems);
            }
        }

        // GET finishedProduct/{invoiceNumber}/{material}
        public IHttpActionResult Get([FromUri]string invoiceNumber, [FromUri]string material)
        {
            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<FinishedProductEntity>(invoiceNumber, material);

            // Execute the retrieve operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            // Check if it was found
            if (retrievedResult.Result == null)
            {
                return this.NotFound();
            }

            FinishedProductEntity entity = (FinishedProductEntity)retrievedResult.Result;
            FinishedProductResource resource = new FinishedProductResource()
            {
                Material = entity.Material,
                Quantity = entity.Quantity,
                FinishDate = entity.FinishDate,
                Location = entity.Location,
                InvoiceNumber = entity.InvoiceNumber
            };

            return this.Ok(resource);
        }

        // GET finishedProduct/{invoiceNumber}
        public IEnumerable<FinishedProductResource> Get([FromUri]string invoiceNumber)
        {
            // Construct the query operation for all customer entities where PartitionKey={partitionKey}.
            TableQuery<FinishedProductEntity> query = new TableQuery<FinishedProductEntity>().Where(
                TableQuery.GenerateFilterCondition(Constants.PartitionKey, QueryComparisons.Equal, invoiceNumber));

            List<FinishedProductResource> itemsByDepartment = new List<FinishedProductResource>();
            foreach (FinishedProductEntity entity in table.ExecuteQuery(query))
            {
                itemsByDepartment.Add(new FinishedProductResource()
                {
                    Material = entity.Material,
                    Quantity = entity.Quantity,
                    FinishDate = entity.FinishDate,
                    Location = entity.Location,
                    InvoiceNumber = entity.InvoiceNumber
                });
            }

            return itemsByDepartment;
        }

        // POST finishedProduct
        public IHttpActionResult Post([FromBody] FinishedProductResource resource)
        {
            FinishedProductEntity entity = new FinishedProductEntity()
            {
                Material = resource.Material,
                Quantity = resource.Quantity,
                FinishDate = resource.FinishDate,
                Location = resource.Location,
                InvoiceNumber = resource.InvoiceNumber
            };

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(entity);

            // Execute the insert operation.
            table.Execute(insertOperation);

            return this.Ok();
        }
    }
}