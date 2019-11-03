using Microsoft.EntityFrameworkCore;

namespace CompanyXPTO.ToggleService.DataAccess.Repositories.Interfaces
{
    /// <summary>
    ///  Defines the IRepositoryFactory interface.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Creates the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="uow">The unit of work.</param>
        /// <returns></returns>
        IRepository<TEntity> CreateRepository<TEntity>(IUnitOfWork<DbContext> uow) where TEntity : class;
    }
}