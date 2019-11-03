using System.Data.Entity;
using CompanyXPTO.ToggleService.Model;

namespace CompanyXPTO.ToggleService.DataAccess.Repositories
{
    /// <summary>
    /// Defines the repository from the Toggle entity.
    /// </summary>
    /// <seealso cref="Repository{TEntity}.ToggleService.Model.Toggle}" />
    public sealed class ToogleRepository : Repository<Toggle>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToogleRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ToogleRepository(DbContext context) : base(context)
        {
        }
    }
}
