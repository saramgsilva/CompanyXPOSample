using System;
using Microsoft.EntityFrameworkCore;
using CompanyXPTO.ToggleService.DataAccess.Repositories.Interfaces;

namespace CompanyXPTO.ToggleService.DataAccess.Repositories
{
    /// <summary>
    ///  Define the RepositoryFactory entity, where is configured all repositories used in this platform.
    /// </summary>
    /// <seealso cref="CompanyXPTO.ToggleService.DataAccess.Repositories.Interfaces.IRepositoryFactory" />
    public class RepositoryFactory : IRepositoryFactory
    {
        /// <summary>
        /// Creates the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="uow">The unit of work.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">uow</exception>
        public IRepository<TEntity> CreateRepository<TEntity>(IUnitOfWork<DbContext> uow) where TEntity : class
        {
            if (uow == null)
            {
                throw new ArgumentNullException("uow");
            }

            switch (typeof(TEntity).Name)
            {
                case "Toggle":
                    return new ToogleRepository(uow.DbContext) as IRepository<TEntity>;
                case "Application":
                    return new ApplicationRepository(uow.DbContext) as IRepository<TEntity>;
                case "ToggleConfig":
                    return new ToggleConfigRepository(uow.DbContext) as IRepository<TEntity>;
                default:
                   return new Repository<TEntity>(uow.DbContext);
            }
        }
    }
}