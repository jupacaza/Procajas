using Microsoft.VisualStudio.TestTools.UnitTesting;
using Procajas.Contracts;
using Procajas.Service.Controllers;
using System.Web.Http.Results;
using System.Linq;

namespace Test.Procajas.Service
{
    [TestClass]
    public class TestAdminItemsController : TestBase
    {
        private static AdminItemController Controller;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            AzureStorageEmulatorStart();
            Controller = new AdminItemController();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            AzureStorageEmulatorClearAll();
        }

        [TestMethod]
        public void Add1AdminItemAndCheck()
        {
            AdminItemResource resource = new AdminItemResource()
            {
                Name = "ABC",
                Type = "Process"
            };

            var actionResult = Controller.Post(resource);

            Assert.IsInstanceOfType(actionResult, typeof(OkResult));

            var responseContent = Controller.Get(resource.Type);

            Assert.IsTrue(responseContent.Any(r => r.Name == resource.Name && r.Type == resource.Type));
        }

        [TestMethod]
        public void Add1AdminItemAndDelete()
        {
            AdminItemResource resource = new AdminItemResource()
            {
                Name = "DEF",
                Type = "Material"
            };

            var actionResult = Controller.Post(resource);

            Assert.IsInstanceOfType(actionResult, typeof(OkResult));

            var responseContent = Controller.Get(resource.Type);

            Assert.IsTrue(responseContent.Any(r => r.Name == resource.Name && r.Type == resource.Type));

            actionResult = Controller.Delete(resource.Type, resource.Name);

            Assert.IsInstanceOfType(actionResult, typeof(OkResult));

            responseContent = Controller.Get(resource.Type);

            Assert.IsFalse(responseContent.Any(r => r.Name == resource.Name && r.Type == resource.Type));
        }
    }
}
