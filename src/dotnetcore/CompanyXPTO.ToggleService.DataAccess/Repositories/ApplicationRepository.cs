using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CompanyXPTO.ToggleService.Model;

namespace CompanyXPTO.ToggleService.DataAccess.Repositories
{
    /// <summary>
    ///  Defines the repository from the Application entity.
    /// </summary>
    /// <seealso cref="Repository{TEntity}.ToggleService.Model.Application}" />
    public sealed class ApplicationRepository : Repository<Application>
    {
        private readonly DbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ApplicationRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the applications.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public IEnumerable<Application> GetApplications(List<string> ids)
        {
            return _context.Set<Application>().Where(a => ids.Contains(a.Id)).Include(a=>a.Configs);
        }
    }
}