using Microsoft.VisualStudio.TestTools.UnitTesting;
using Procajas.Contracts;
using Procajas.Service.Controllers;
using System;
using System.Web.Http.Results;

namespace Test.Procajas.Service
{
    [TestClass]
    public class TestWarehouseController : TestBase
    {
        private static WarehouseController Controller;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            AzureStorageEmulatorStart();
            Controller = new WarehouseController();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            AzureStorageEmulatorClearAll();
        }

        [TestMethod]
        public void Add1WarehouseResourceAndCheck()
        {
            WarehouseResource resource = new WarehouseResource()
            {
                Material = "IMP_GordaBebe",
                Department = "IMP",
                Quantity = 1000,
                Location = "A1",
                DateOfInsertion = DateTime.Now,
                InvoiceNumber = "A12345B"
            };

            var actionResult = Controller.Post(resource);

            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IdResource>));
            
            IdResource idResource = ((OkNegotiatedContentResult<IdResource>)actionResult).Content;
            resource.Id = idResource.Id;

            actionResult = Controller.Get("IMP", idResource.Id);

            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<WarehouseResource>));

            WarehouseResource warehouseResource = ((OkNegotiatedContentResult<WarehouseResource>)actionResult).Content;

            Assert.AreEqual(resource.Id, warehouseResource.Id);
            Assert.AreEqual(resource.Material, resource.Material);
            Assert.AreEqual(resource.Department, warehouseResource.Department);
            Assert.AreEqual(resource.Quantity, warehouseResource.Quantity);
            Assert.AreEqual(resource.Location, warehouseResource.Location);
            Assert.AreEqual(resource.DateOfInsertion.ToUniversalTime(), warehouseResource.DateOfInsertion.ToUniversalTime());
            Assert.AreEqual(resource.InvoiceNumber, warehouseResource.InvoiceNumber);
        }
    }
}
