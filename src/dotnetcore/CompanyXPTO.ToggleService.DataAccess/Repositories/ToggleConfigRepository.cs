using Microsoft.EntityFrameworkCore;
using CompanyXPTO.ToggleService.Model;

namespace CompanyXPTO.ToggleService.DataAccess.Repositories
{
    /// <summary>
    ///  Defines the repository from the ToggleConfig entity.
    /// </summary>
    /// <seealso cref="Repository{TEntity}.ToggleService.Model.ToggleConfig}" />
    public sealed class ToggleConfigRepository : Repository<ToggleConfig>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleConfigRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ToggleConfigRepository(DbContext context) : base(context)
        {
        }
    }
}