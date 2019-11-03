using System;
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
    public class ToggleBusinessManagerTest
    {
        [TestMethod]
        public async Task GetTogglesAsync_GetResponseWith2ToggleDtos_Ok()
        {
            var toggles = ToggleFakeData.GetToggles();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Toggle>>();
            repositoryMock.Setup(r => r.GetAsync()).Returns(Task.FromResult(toggles));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Toggle>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ToggleBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);

            var response = await manager.GetTogglesAsync();
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.IsValid);
            Assert.AreEqual(response.Result.Count(),2);
            foreach (var item in response.Result)
            {
                Assert.IsNotNull(item.Name);
                Assert.IsNotNull(item.Id);
                Assert.IsNotNull(item.Applications);
            }
        }

        [TestMethod]
        public async Task GetTogglesAsync_Ok()
        {
            var toogle = ToggleFakeData.GetToggles().FirstOrDefault();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Toggle>>();
            repositoryMock.Setup(r => r.FindIdAsync(toogle.Id)).Returns(Task.FromResult(toogle));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Toggle>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ToggleBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            var response = await manager.GetToggleAsync(toogle.Id);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.IsValid);
            Assert.AreEqual(response.Result.Id, toogle.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetTogglesAsyncc_WithoutId()
        {
            var toogle = ToggleFakeData.GetToggles().FirstOrDefault();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Toggle>>();
            repositoryMock.Setup(r => r.FindIdAsync(toogle.Id)).Returns(Task.FromResult(toogle));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Toggle>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ToggleBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            await manager.GetToggleAsync(string.Empty);
        }
        
        [TestMethod]
        public async Task GetToggleAsync_Ok()
        {
            var toggle = ToggleFakeData.GetToggle1();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Toggle>>();
            repositoryMock.Setup(r => r.FindIdAsync(toggle.Id)).Returns(Task.FromResult(toggle));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Toggle>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ToggleBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            var response = await manager.GetToggleAsync(toggle.Id);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.IsValid);
            Assert.AreEqual(response.Result.Id, toggle.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetToggleAsync_WithoutId()
        {
            var toggle = ToggleFakeData.GetToggle1();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Toggle>>();
            repositoryMock.Setup(r => r.FindIdAsync(toggle.Id)).Returns(Task.FromResult(toggle));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Toggle>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ToggleBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            await manager.GetToggleAsync(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task GetToggleAsync_ToogleIdNotExists()
        {
            var toggle = ToggleFakeData.GetToggle1();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Toggle>>();
            repositoryMock.Setup(r => r.FindIdAsync(toggle.Id)).Returns(Task.FromResult(default(Toggle)));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Toggle>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ToggleBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            await manager.GetToggleAsync(toggle.Id);
        }

        [TestMethod]
        public void DeleteToggleAsync_Ok()
        {
            var toggle = ToggleFakeData.GetToggle1();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Toggle>>();
            repositoryMock.Setup(r => r.GetById(toggle.Id)).Returns(toggle);
            unitoOfWorkMock.Setup(r => r.Save());

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Toggle>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ToggleBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            manager.DeleteToggle(toggle.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DeleteToggleAsync_ToogleIdNotExists()
        {
            var toggle = ToggleFakeData.GetToggle1();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Toggle>>();
            repositoryMock.Setup(r => r.GetById(toggle.Id)).Returns(default(Toggle));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Toggle>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ToggleBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
             manager.DeleteToggle(toggle.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteToggleAsync_WithoutId()
        {
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Toggle>>();

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Toggle>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ToggleBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            manager.DeleteToggle(string.Empty);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task PostToggleAsync_WithoutToggleDto()
        {
            var toggle = ToggleFakeData.GetToggle1();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Toggle>>();
            repositoryMock.Setup(r => r.FindIdAsync(toggle.Id)).Returns(Task.FromResult(toggle));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Toggle>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ToggleBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            await manager.PostToggleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task PostToggleAsync_WithoutToggleDtoId()
        {
            var toggle = ToggleFakeData.GetToggleDto1();
            toggle.Id = string.Empty;
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Toggle>>();
            
            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Toggle>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ToggleBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            await manager.PostToggleAsync(toggle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task PutToggleAsync_WithoutToggleDto()
        {
            var toggle = ToggleFakeData.GetToggle1();
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            var repositoryMock = new Mock<IRepository<Toggle>>();
            repositoryMock.Setup(r => r.FindIdAsync(toggle.Id)).Returns(Task.FromResult(toggle));

            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(r => r.CreateRepository<Toggle>(unitoOfWorkMock.Object)).Returns(repositoryMock.Object);

            var manager = new ToggleBusinessManager(unitoOfWorkMock.Object, repositoryFactoryMock.Object);
            await manager.PutToggleAsync(null);
        }
    }
}