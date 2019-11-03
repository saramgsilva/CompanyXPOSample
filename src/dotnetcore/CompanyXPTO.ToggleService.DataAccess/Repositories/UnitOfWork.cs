using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CompanyXPTO.ToggleService.DataAccess.Repositories.Interfaces;

namespace CompanyXPTO.ToggleService.DataAccess.Repositories
{
    /// <summary>
    /// Defines the Unit of work entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="CompanyXPTO.ToggleService.DataAccess.Repositories.Interfaces.IUnitOfWork{T}" />
    /// <seealso cref="System.IDisposable" />
    public class UnitOfWork<T>: IUnitOfWork<T>, IDisposable where T : DbContext
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{T}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public UnitOfWork(T dbContext)
        {
            _disposed = false;
            DbContext = dbContext;
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        public T DbContext { get; private set; }

        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveAsync()
        {
           return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}