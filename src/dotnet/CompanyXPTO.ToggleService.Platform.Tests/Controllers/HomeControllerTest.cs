using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompanyXPTO.ToggleService.Platform.Controllers;

namespace CompanyXPTO.ToggleService.Platform.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}