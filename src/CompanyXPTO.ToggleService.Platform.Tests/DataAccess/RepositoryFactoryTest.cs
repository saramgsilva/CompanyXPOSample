using System;
using System.Data.Entity;
using CompanyXPTO.ToggleService.DataAccess.Repositories;
using CompanyXPTO.ToggleService.DataAccess.Repositories.Interfaces;
using CompanyXPTO.ToggleService.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyXPTO.ToggleService.Platform.Tests.DataAccess
{
    [TestClass]
    public class RepositoryFactoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateRepository_WithoutUnitOfWork()
        {
          new RepositoryFactory().CreateRepository<Toggle>(null);
        }

        [TestMethod]
        public void CreateRepository_ToggleOk()
        {
            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(r => r.Set<Toggle>());
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            unitoOfWorkMock.Setup(r => r.DbContext).Returns(dbContextMock.Object);
            var repository = new RepositoryFactory().CreateRepository<Toggle>(unitoOfWorkMock.Object);
            Assert.IsTrue(repository is ToogleRepository);
        }

        [TestMethod]
        public void CreateRepository_ApplicationOk()
        {
            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(r => r.Set<Application>());
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            unitoOfWorkMock.Setup(r => r.DbContext).Returns(dbContextMock.Object);
            var repository = new RepositoryFactory().CreateRepository<Application>(unitoOfWorkMock.Object);
            Assert.IsTrue(repository is ApplicationRepository);
        }

        [TestMethod]
        public void CreateRepository_ToggleConfigOk()
        {
            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(r => r.Set<ToggleConfig>());
            var unitoOfWorkMock = new Mock<IUnitOfWork<DbContext>>();
            unitoOfWorkMock.Setup(r => r.DbContext).Returns(dbContextMock.Object);
            var repository = new RepositoryFactory().CreateRepository<ToggleConfig>(unitoOfWorkMock.Object);
            Assert.IsTrue(repository is ToggleConfigRepository);
        }
    }
}
