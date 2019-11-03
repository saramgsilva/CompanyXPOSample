using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ApplicationControllerTest
    {
        #region GetApplicationsAsync
        [TestMethod]
        public async Task GetApplicationsAsync_GetResponseWith2ApplicationsDtos_Ok()
        {
            var mock = new Mock<IApplicationBusinessManager>();
            mock.Setup(applicationBusinessManager => applicationBusinessManager.GetApplicationsAsync()).Returns(Task.FromResult(ApplicationFakeData.GetResponseWith2ApplicationDtos()));

            using (var controller = new ApplicationController(mock.Object))
            {
                var responseActionResult = await controller.GetApplicationsAsync() as OkNegotiatedContentResult<Response<IEnumerable<ApplicationDto>>>;

                Assert.IsNotNull(responseActionResult);
                Assert.IsNotNull(responseActionResult);
                Assert.IsNull(responseActionResult.Content.ErrorCode);
                Assert.IsNull(responseActionResult.Content.Message);
                Assert.IsTrue(responseActionResult.Content.IsValid);
                Assert.IsTrue(responseActionResult.Content.Result.Count() == 2);
                foreach (var app in responseActionResult.Content.Result)
                {
                    Assert.IsNotNull(app.Id);
                    Assert.IsNotNull(app.Name);
                }
            }
        }

        [TestMethod]
        public async Task GetApplicationsAsync_GetResponseWithoutApplicationsDtos_Ok()
        {
            var mock = new Mock<IApplicationBusinessManager>();
            mock.Setup(applicationBusinessManager => applicationBusinessManager.GetApplicationsAsync()).Returns(Task.FromResult(ApplicationFakeData.GetResponseWithoutApplicationDtos()));

            using (var controller = new ApplicationController(mock.Object))
            {
                var responseActionResult = await controller.GetApplicationsAsync() as OkNegotiatedContentResult<Response<IEnumerable<ApplicationDto>>>;

                Assert.IsNotNull(responseActionResult);
                Assert.IsNotNull(responseActionResult);
                Assert.IsNull(responseActionResult.Content.ErrorCode);
                Assert.IsNull(responseActionResult.Content.Message);
                Assert.IsTrue(responseActionResult.Content.IsValid);
                Assert.IsNotNull(responseActionResult.Content.Result);
                Assert.AreEqual(responseActionResult.Content.Result.Count(), 0);
            }
        }

        [TestMethod]
        public async Task GetApplicationsAsync_GetResponseWithErrorCodeAndMessage_Ok()
        {
            var mock = new Mock<IApplicationBusinessManager>();
            mock.Setup(applicationBusinessManager => applicationBusinessManager.GetApplicationsAsync()).Returns(Task.FromResult(ApplicationFakeData.GetResponseWithErrorCodeAndMessage()));

            using (var controller = new ApplicationController(mock.Object))
            {
                var responseActionResult = await controller.GetApplicationsAsync() as OkNegotiatedContentResult<Response<IEnumerable<ApplicationDto>>>;

                Assert.IsNotNull(responseActionResult);
                Assert.IsNotNull(responseActionResult);
                Assert.IsNotNull(responseActionResult.Content.ErrorCode);
                Assert.IsNotNull(responseActionResult.Content.Message);
                Assert.IsFalse(responseActionResult.Content.IsValid);
                Assert.IsNull(responseActionResult.Content.Result);
            }
        }

        [TestMethod]
        public async Task GetApplicationsAsync_InternalServerError()
        {
            var mock = new Mock<IApplicationBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.GetApplicationsAsync()).Throws(new NullReferenceException());

            using (var controller = new ApplicationController(mock.Object))
            {
                var responseActionResult = (await controller.GetApplicationsAsync()) as ExceptionResult;
                Assert.IsNotNull(responseActionResult);
                Assert.IsTrue(responseActionResult.Exception is NullReferenceException);
            }
        }
        #endregion

        #region GetApplicationAsync(string applicationId)

        [TestMethod]
        public async Task GetApplicationAsync_InternalServerError()
        {
            var id = Guid.NewGuid().ToString();
            var mock = new Mock<IApplicationBusinessManager>();
            mock.Setup(applicationBusinessManager => applicationBusinessManager.GetApplicationAsync(id)).Throws(new NullReferenceException());

            using (var controller = new ApplicationController(mock.Object))
            {
                var responseActionResult = (await controller.GetApplicationAsync(id)) as ExceptionResult;
                Assert.IsNotNull(responseActionResult);
                Assert.IsTrue(responseActionResult.Exception is NullReferenceException);
            }
        }

        [TestMethod]
        public async Task GetApplicationAsync_BadRequest()
        {
            var id = string.Empty;
            var mock = new Mock<IApplicationBusinessManager>();
            mock.Setup(applicationBusinessManager => applicationBusinessManager.GetApplicationAsync(id)).Returns(Task.FromResult(new Response<ApplicationDto>()));

            using (var controller = new ApplicationController(mock.Object))
            {
                var responseActionResult = (await controller.GetApplicationAsync(id)) as BadRequestResult;
                Assert.IsNotNull(responseActionResult);
            }
        }

        [TestMethod]
        public async Task GetApplicationAsync_NotFound()
        {
            var id = "12345";
            var mock = new Mock<IApplicationBusinessManager>();
            mock.Setup(applicationBusinessManager => applicationBusinessManager.GetApplicationAsync(id)).Throws(new ArgumentOutOfRangeException());

            using (var controller = new ApplicationController(mock.Object))
            {
                var responseActionResult = (await controller.GetApplicationAsync(id)) as NotFoundResult;
                Assert.IsNotNull(responseActionResult);
            }
        }

        [TestMethod]
        public async Task GetApplicationAsync_Ok()
        {
            var abc = ApplicationFakeData.GetApplicationDtoABC();

            var mock = new Mock<IApplicationBusinessManager>();
            mock.Setup(applicationBusinessManager => applicationBusinessManager.GetApplicationAsync(abc.Id)).Returns(Task.FromResult(ApplicationFakeData.GetResponseWithAbcApplicationDto()));

            using (var controller = new ApplicationController(mock.Object))
            {
                var responseActionResult = (await controller.GetApplicationAsync(abc.Id)) as OkNegotiatedContentResult<Response<ApplicationDto>>;
                Assert.IsNotNull(responseActionResult);
                Assert.IsNotNull(responseActionResult.Content);
                Assert.IsNotNull(responseActionResult.Content.Result);
                Assert.IsNotNull(responseActionResult.Content.Result.Id);
                Assert.IsNotNull(responseActionResult.Content.Result.Name);

                Assert.AreEqual(abc.Id, responseActionResult.Content.Result.Id);
                Assert.AreEqual(abc.Name, responseActionResult.Content.Result.Name);
            }
        }
        #endregion
    }
}