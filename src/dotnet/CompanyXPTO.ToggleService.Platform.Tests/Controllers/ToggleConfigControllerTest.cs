using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using CompanyXPTO.ToggleService.Core.Interfaces;
using CompanyXPTO.ToggleService.Dtos;
using CompanyXPTO.ToggleService.Platform.Controllers;
using CompanyXPTO.ToggleService.Platform.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyXPTO.ToggleService.Platform.Tests.Controllers
{
    [TestClass]
    public class ToggleConfigControllerTest
    {
        #region GetApplicationAsync(string applicationId)

        [TestMethod]
        public async Task GetApplicationAsync_InternalServerError()
        {
            var id = Guid.NewGuid().ToString();
            var mock = new Mock<IApplicationBusinessManager>();
            mock.Setup(applicationBusinessManager => applicationBusinessManager.GetTogglesAsync(id)).Throws(new NullReferenceException());

            using (var controller = new ToggleConfigController(mock.Object))
            {
                var responseActionResult = (await controller.GetTogglesAsync(id)) as ExceptionResult;
                Assert.IsNotNull(responseActionResult);
                Assert.IsTrue(responseActionResult.Exception is NullReferenceException);
            }
        }

        [TestMethod]
        public async Task GetApplicationAsync_BadRequest()
        {
            var id = string.Empty;
            var mock = new Mock<IApplicationBusinessManager>();
            mock.Setup(applicationBusinessManager => applicationBusinessManager.GetTogglesAsync(id)).Returns(Task.FromResult(new Response<IEnumerable<ToggleServiceConfigDto>>()));

            using (var controller = new ToggleConfigController(mock.Object))
            {
                var responseActionResult = (await controller.GetTogglesAsync(id)) as BadRequestResult;
                Assert.IsNotNull(responseActionResult);
            }
        }

        [TestMethod]
        public async Task GetApplicationAsync_NotFound()
        {
            var id = "12345";
            var mock = new Mock<IApplicationBusinessManager>();
            mock.Setup(applicationBusinessManager => applicationBusinessManager.GetTogglesAsync(id)).Throws(new ArgumentOutOfRangeException());

            using (var controller = new ToggleConfigController(mock.Object))
            {
                var responseActionResult = (await controller.GetTogglesAsync(id)) as NotFoundResult;
                Assert.IsNotNull(responseActionResult);
            }
        }

        [TestMethod]
        public async Task GetApplicationAsync_Ok()
        {
            var abc = ApplicationFakeData.GetApplicationDtoABC();

            var mock = new Mock<IApplicationBusinessManager>();
            mock.Setup(applicationBusinessManager => applicationBusinessManager.GetTogglesAsync(abc.Id)).Returns(Task.FromResult(ToggleServiceConfigFakeData.GetResponseWith2ToggleServiceConfigDto()));

            using (var controller = new ToggleConfigController(mock.Object))
            {
                var responseActionResult = (await controller.GetTogglesAsync(abc.Id)) as OkNegotiatedContentResult<Response<IEnumerable<ToggleServiceConfigDto>>>;
                Assert.IsNotNull(responseActionResult);
                Assert.IsNotNull(responseActionResult.Content);
                Assert.IsNotNull(responseActionResult.Content.Result);

                foreach (var item in responseActionResult.Content.Result)
                {
                    Assert.IsNotNull(item.Id);
                    Assert.IsNotNull(item.Version);
                }
            }
        }
        #endregion
    }
}
