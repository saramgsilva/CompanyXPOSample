using System.Data.Entity;
using System.Threading.Tasks;

namespace CompanyXPTO.ToggleService.DataAccess.Repositories.Interfaces
{
    /// <summary>
    /// Defines the IUnitOfWork interface.
    /// </summary>
    /// <typeparam name="T">{T} is a DbContext</typeparam>
    public interface IUnitOfWork<T> where T : DbContext
    {
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        T DbContext { get; }

        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveAsync();

        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();
    }
}