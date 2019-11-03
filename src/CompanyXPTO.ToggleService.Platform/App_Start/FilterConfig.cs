using System.Web;
using System.Web.Mvc;

namespace CompanyXPTO.ToggleService.Platform
{
    /// <summary>
    ///  Defines the filters configuration.
    /// </summary>
    public sealed class FilterConfig
    {
        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
