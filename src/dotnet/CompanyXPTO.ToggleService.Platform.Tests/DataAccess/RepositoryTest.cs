using System;
using System.Data.Entity;
using System.Threading.Tasks;
using CompanyXPTO.ToggleService.DataAccess.Repositories;
using CompanyXPTO.ToggleService.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyXPTO.ToggleService.Platform.Tests.DataAccess
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public async Task FindIdAsync_Ok()
        {
            var dbSetMock = new Mock<DbSet<Toggle>>();
            dbSetMock.Setup(r => r.FindAsync("1234")).Returns(Task.FromResult(new Toggle()));

            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(r => r.Set<Toggle>()).Returns(dbSetMock.Object);

            var repository = new Repository<Toggle>(dbContextMock.Object);
            var result = await repository.FindIdAsync("1234");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task FindIdAsync_WithoutId()
        {
            var dbSetMock = new Mock<DbSet<Toggle>>();
            dbSetMock.Setup(r => r.FindAsync(string.Empty)).Returns(Task.FromResult(default(Toggle)));

            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(r => r.Set<Toggle>()).Returns(dbSetMock.Object);

            var repository = new Repository<Toggle>(dbContextMock.Object);
            await repository.FindIdAsync(string.Empty);
        }

        [TestMethod]
        public async Task FindIdAsync_NotExist()
        {
            var dbSetMock = new Mock<DbSet<Toggle>>();
            dbSetMock.Setup(r => r.FindAsync("1234")).Returns(Task.FromResult(default(Toggle)));

            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(r => r.Set<Toggle>()).Returns(dbSetMock.Object);

            var repository = new Repository<Toggle>(dbContextMock.Object);
            var result = await repository.FindIdAsync("1234");
            Assert.IsNull(result);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_WithoutId()
        {
            var dbSetMock = new Mock<DbSet<Toggle>>();
            dbSetMock.Setup(r => r.Find(string.Empty)).Returns(default(Toggle));

            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(r => r.Set<Toggle>()).Returns(dbSetMock.Object);

            var repository = new Repository<Toggle>(dbContextMock.Object);
            repository.Delete(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task Delete_NotExist()
        {
            var dbSetMock = new Mock<DbSet<Toggle>>();
            dbSetMock.Setup(r => r.Find("1234")).Returns(default(Toggle));

            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(r => r.Set<Toggle>()).Returns(dbSetMock.Object);

            var repository = new Repository<Toggle>(dbContextMock.Object);
            repository.Delete("1234");
        }
    }
}