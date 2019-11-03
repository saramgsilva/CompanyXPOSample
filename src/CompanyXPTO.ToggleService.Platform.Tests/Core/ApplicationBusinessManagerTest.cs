using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CompanyXPTO.ToggleService.Core;
using CompanyXPTO.ToggleService.DataAccess.Repositories.Interfaces;
using CompanyXPTO.ToggleService.Model;
using CompanyXPTO.ToggleService.Platform.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyXPTO.ToggleService.Platform.Tests.Core
{
    [TestClass]
    public class ApplicationBusinessManagerTest
    {
        [TestMethod]
        public async Task GetApplicationsAsync_GetResponseWith2ApplicationDtos_Ok()
        {
            var apps = ApplicationFakeData.GetApplications();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Application>>();
            repositoryMock.Setup(r => r.GetAsync()).Returns(Task.FromResult(apps));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Application>(unitoOfWorkMock.Object))
                .Returns(repositoryMock.Object);

            var manager = new ApplicationBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);

            var response = await manager.GetApplicationsAsync();
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.IsValid);
            foreach (var item in response.Result)
            {
                Assert.IsNotNull(item.Name);
                Assert.IsNotNull(item.Id);
            }
        }

        [TestMethod]
        public async Task GetApplicationsAsync_GetResponseWithoutApplicationDtos_Ok()
        {
            var apps = ApplicationFakeData.GetWithoutApplications();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Application>>();
            repositoryMock.Setup(r => r.GetAsync()).Returns(Task.FromResult(apps));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Application>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ApplicationBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);

            var response = await manager.GetApplicationsAsync();
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.IsValid);
            Assert.AreEqual(response.Result.Count(), 0);
        }

        [TestMethod]
        public async Task GetApplicationAsync_Ok()
        {
            var app = ApplicationFakeData.GetApplications().FirstOrDefault();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Application>>();
            repositoryMock.Setup(r => r.FindIdAsync(app.Id)).Returns(Task.FromResult(app));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Application>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ApplicationBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            var response = await manager.GetApplicationAsync(app.Id);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsValid);
            Assert.AreEqual(response.Result.Id, app.Id);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetApplicationsAsync_WithoutId()
        {
            var app = ApplicationFakeData.GetApplications().FirstOrDefault();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Application>>();
            repositoryMock.Setup(r => r.FindIdAsync(app.Id)).Returns(Task.FromResult(app));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Application>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ApplicationBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            await manager.GetApplicationAsync(string.Empty);
        }
        
        [TestMethod]
        public async Task GetToggleAsync_Ok()
        {
            var app = ApplicationFakeData.GetApplications().FirstOrDefault();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Application>>();
            repositoryMock.Setup(r => r.FindIdAsync(app.Id)).Returns(Task.FromResult(app));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Application>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ApplicationBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            var response = await manager.GetTogglesAsync(app.Id);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsValid);
            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        public async Task GetToggleAsync_WithoutToggleSerivcePermission()
        {
            var app = ApplicationFakeData.GetApplications().FirstOrDefault();
            app.IsToggleServiceAllowed = false;
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Application>>();
            repositoryMock.Setup(r => r.FindIdAsync(app.Id)).Returns(Task.FromResult(app));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Application>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ApplicationBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            var response = await manager.GetTogglesAsync(app.Id);
            Assert.IsNotNull(response);
            Assert.IsFalse(response.IsValid);
            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.ErrorCode);
            Assert.IsNotNull(response.Message);
        }

        [TestMethod]
        public async Task GetTogglesAsync_WithConfigs_Ok()
        {
            var app = ApplicationFakeData.GetApplications().FirstOrDefault();
            app.Configs = new List<ToggleConfig>
            {
                new ToggleConfig
                {
                    Id=Guid.NewGuid().ToString(),
                    Version = "v1",
                    ApplicationId = app.Id,
                    ToggleId = Guid.NewGuid().ToString()
                }
            };
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Application>>();
            repositoryMock.Setup(r => r.FindIdAsync(app.Id)).Returns(Task.FromResult(app));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Application>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ApplicationBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            var response = await manager.GetTogglesAsync(app.Id);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.IsValid);
            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task GetTogglesAsync_ApplicationIdNotExists()
        {
            var app = ApplicationFakeData.GetApplications().FirstOrDefault();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Application>>();
            repositoryMock.Setup(r => r.FindIdAsync(app.Id)).Returns(Task.FromResult(default(Application)));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Application>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ApplicationBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            await manager.GetTogglesAsync(app.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetTogglesAsync_WithoutId()
        {
            var app = ApplicationFakeData.GetApplications().FirstOrDefault();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Application>>();
            repositoryMock.Setup(r => r.FindIdAsync(app.Id)).Returns(Task.FromResult(app));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Application>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ApplicationBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            await manager.GetTogglesAsync(string.Empty);
        }
    }
}