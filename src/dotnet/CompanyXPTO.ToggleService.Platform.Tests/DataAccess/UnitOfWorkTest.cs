using System.Data.Entity;
using System.Threading.Tasks;
using CompanyXPTO.ToggleService.DataAccess.Repositories;
using CompanyXPTO.ToggleService.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyXPTO.ToggleService.Platform.Tests.DataAccess
{
    [TestClass]
    public class UnitOfWorkTest
    {
        [TestMethod]
        public void GetDbContext_Ok()
        {
            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(r => r.Set<Toggle>());

            using (var unitOfWork = new UnitOfWork<DbContext>(dbContext: dbContextMock.Object))
            {
                Assert.IsNotNull(unitOfWork.DbContext);
            }
        }

        [TestMethod]
        public void Save_Ok()
        {
            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(r => r.SaveChanges());

            using (var unitOfWork = new UnitOfWork<DbContext>(dbContext: dbContextMock.Object))
            {
                unitOfWork.Save();
            }
        }

        [TestMethod]
        public async Task SaveAsync_Ok()
        {
            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(r => r.SaveChangesAsync()).Returns(Task.FromResult(1));

            using (var unitOfWork = new UnitOfWork<DbContext>(dbContext: dbContextMock.Object))
            {
                await unitOfWork.SaveAsync();
            }
        }
    }
}