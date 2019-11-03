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
    public class ToggleControllerTest
    {
        #region  GetTogglesAsync
        [TestMethod]
        public async Task GetTogglesAsync_GetResponseWith2ToggleDtos_Ok()
        {
            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.GetTogglesAsync()).Returns(Task.FromResult(ToggleFakeData.GetResponseWith2ToggleDtos()));

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = await controller.GetTogglesAsync() as OkNegotiatedContentResult<Response<IEnumerable<ToggleDto>>>;

                Assert.IsNotNull(responseActionResult);
                Assert.IsNotNull(responseActionResult);
                Assert.IsNull(responseActionResult.Content.ErrorCode);
                Assert.IsNull(responseActionResult.Content.Message);
                Assert.IsTrue(responseActionResult.Content.IsValid);
                Assert.IsTrue(responseActionResult.Content.Result.Count() == 2);
                foreach (var item in responseActionResult.Content.Result)
                {
                    Assert.IsNotNull(item.Id);
                    Assert.IsNotNull(item.Name);
                    Assert.IsNotNull(item.Version);
                    Assert.IsNotNull(item.Applications);
                    foreach (var app in item.Applications)
                    {
                        Assert.IsNotNull(app.Id);
                        Assert.IsNotNull(app.Name);
                    }
                }
            }
        }

        [TestMethod]
        public async Task GetTogglesAsync_GetResponseWithoutToggleDtos_Ok()
        {
            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.GetTogglesAsync()).Returns(Task.FromResult(ToggleFakeData.GetResponseWithoutToggleDtos()));

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = await controller.GetTogglesAsync() as OkNegotiatedContentResult<Response<IEnumerable<ToggleDto>>>;

                Assert.IsNotNull(responseActionResult);
                Assert.IsNotNull(responseActionResult);
                Assert.IsNull(responseActionResult.Content.ErrorCode);
                Assert.IsNull(responseActionResult.Content.Message);
                Assert.IsTrue(responseActionResult.Content.IsValid);
                Assert.IsNotNull(responseActionResult.Content.Result);
                Assert.AreEqual(responseActionResult.Content.Result.Count(),0);
            }
        }
        
        [TestMethod]
        public async Task GetTogglesAsync_GetResponseWithErrorCodeAndMessage_Ok()
        {
            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.GetTogglesAsync()).Returns(Task.FromResult(ToggleFakeData.GetResponseWithErrorCodeAndMessage()));

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = await controller.GetTogglesAsync() as OkNegotiatedContentResult<Response<IEnumerable<ToggleDto>>>;

                Assert.IsNotNull(responseActionResult);
                Assert.IsNotNull(responseActionResult);
                Assert.IsNotNull(responseActionResult.Content.ErrorCode);
                Assert.IsNotNull(responseActionResult.Content.Message);
                Assert.IsFalse(responseActionResult.Content.IsValid);
                Assert.IsNull(responseActionResult.Content.Result);
            }
        }

        [TestMethod]
        public async Task GetTogglesAsync_InternalServerError()
        {
            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.GetTogglesAsync()).Throws(new NullReferenceException());

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = (await controller.GetTogglesAsync()) as ExceptionResult;
                Assert.IsNotNull(responseActionResult);
                Assert.IsTrue(responseActionResult.Exception is NullReferenceException);
            }
        }
        #endregion

        #region GetToggledAsync(string applicationId)
        [TestMethod]
        public async Task GetToggledAsyncc_InternalServerError()
        {
            var id = Guid.NewGuid().ToString();
            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.GetToggleAsync(id)).Throws(new NullReferenceException());

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = (await controller.GetToggleAsync(id)) as ExceptionResult;
                Assert.IsNotNull(responseActionResult);
                Assert.IsTrue(responseActionResult.Exception is NullReferenceException);
            }
        }

        [TestMethod]
        public async Task GetToggledAsync_BadRequest()
        {
            var id = string.Empty;
            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.GetToggleAsync(id)).Returns(Task.FromResult(new Response<ToggleDto>()));

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = (await controller.GetToggleAsync(id)) as BadRequestResult;
                Assert.IsNotNull(responseActionResult);
            }
        }

        [TestMethod]
        public async Task GetToggledAsync_NotFound()
        {
            var id = "12345";
            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.GetToggleAsync(id)).Throws(new ArgumentOutOfRangeException());

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = (await controller.GetToggleAsync(id)) as NotFoundResult;
                Assert.IsNotNull(responseActionResult);
            }
        }

        [TestMethod]
        public async Task GetToggledAsync_Ok()
        {
            var toggle1 = ToggleFakeData.GetToggleDto1();

            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.GetToggleAsync(toggle1.Id)).Returns(Task.FromResult(ToggleFakeData.GetResponseWithToggleDto1()));

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = (await controller.GetToggleAsync(toggle1.Id)) as OkNegotiatedContentResult<Response<ToggleDto>>;
                Assert.IsNotNull(responseActionResult);
                Assert.IsNotNull(responseActionResult.Content);
                Assert.IsNotNull(responseActionResult.Content.Result);
                Assert.IsNotNull(responseActionResult.Content.Result.Id);
                Assert.IsNotNull(responseActionResult.Content.Result.Name);

                Assert.AreEqual(toggle1.Id, responseActionResult.Content.Result.Id);
                Assert.AreEqual(toggle1.Name, responseActionResult.Content.Result.Name);
                Assert.AreEqual(toggle1.Version, responseActionResult.Content.Result.Version);
                Assert.AreEqual(toggle1.Applications.Count(), responseActionResult.Content.Result.Applications.Count());
            }
        }
        #endregion

        #region DeleteToggleAsync(string applicationId)
        [TestMethod]
        public void DeleteToggleAsync_InternalServerError()
        {
            var id = Guid.NewGuid().ToString();
            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.DeleteToggle(id)).Throws(new NullReferenceException());

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = controller.DeleteToggle(id) as ExceptionResult;
                Assert.IsNotNull(responseActionResult);
                Assert.IsTrue(responseActionResult.Exception is NullReferenceException);
            }
        }

        [TestMethod]
        public void DeleteToggleAsync_BadRequest()
        {
            var id = string.Empty;
            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.DeleteToggle(id));

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = controller.DeleteToggle(id) as BadRequestResult;
                Assert.IsNotNull(responseActionResult);
            }
        }

        [TestMethod]
        public void DeleteToggleAsync_NotFound()
        {
            var id = "12345";
            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.DeleteToggle(id)).Throws(new ArgumentOutOfRangeException());

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = controller.DeleteToggle(id) as NotFoundResult;
                Assert.IsNotNull(responseActionResult);
            }
        }

        [TestMethod]
        public void DeleteToggleAsync_Ok()
        {
            var toggle1 = ToggleFakeData.GetToggleDto1();

            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.DeleteToggle(toggle1.Id));

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = controller.DeleteToggle(toggle1.Id) as OkResult;
                Assert.IsNotNull(responseActionResult);
            }
        }
        #endregion

        #region PostToggleAsync(string applicationId)
        [TestMethod]
        public async Task PostToggleAsync_Ok()
        {
            var toogleDto = ToggleFakeData.GetToggleDto2();

            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.PostToggleAsync(toogleDto)).Returns(Task.FromResult(new Response<ToggleDto> { IsValid = true, Result = toogleDto }));

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = await controller.PostToggleAsync(toogleDto) as OkNegotiatedContentResult<Response<ToggleDto>>;
                Assert.IsNotNull(responseActionResult);
                Assert.IsTrue(responseActionResult.Content.IsValid);
                Assert.IsNotNull(responseActionResult.Content.Result);
            }
        }

        [TestMethod]
        public async Task PostToggleAsync_InternalServerError()
        {
            var toogleDto = ToggleFakeData.GetToggleDto2();
            var id = Guid.NewGuid().ToString();
            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.PostToggleAsync(toogleDto)).Throws(new NullReferenceException());

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = await controller.PostToggleAsync(toogleDto) as ExceptionResult;
                Assert.IsNotNull(responseActionResult);
                Assert.IsTrue(responseActionResult.Exception is NullReferenceException);
            }
        }
        
        [TestMethod]
        public async Task PostToggleAsync_BadRequest()
        {
            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.PostToggleAsync(null));

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = await controller.PostToggleAsync(null) as BadRequestResult;
                Assert.IsNotNull(responseActionResult);
            }
        }
        #endregion
        
        #region PutToggleAsync(string applicationId)
        [TestMethod]
        public async Task PutToggleAsync_InternalServerError()
        {
            var toogleDto = ToggleFakeData.GetToggleDto2();
            var id = Guid.NewGuid().ToString();
            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.PutToggleAsync(toogleDto)).Throws(new NullReferenceException());

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = await controller.PutToggleAsync(toogleDto) as ExceptionResult;
                Assert.IsNotNull(responseActionResult);
                Assert.IsTrue(responseActionResult.Exception is NullReferenceException);
            }
        }

        [TestMethod]
        public async Task PutToggleAsync_BadRequest()
        {
            var mock = new Mock<IToggleBusinessManager>();
            mock.Setup(toggleBusinessManager => toggleBusinessManager.PutToggleAsync(null));

            using (var controller = new ToggleController(mock.Object))
            {
                var responseActionResult = await controller.PutToggleAsync(null) as BadRequestResult;
                Assert.IsNotNull(responseActionResult);
            }
        }
        #endregion
    }
}