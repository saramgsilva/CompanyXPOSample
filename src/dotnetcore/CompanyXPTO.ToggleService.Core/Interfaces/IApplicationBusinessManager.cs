using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyXPTO.ToggleService.Dtos;

namespace CompanyXPTO.ToggleService.Core.Interfaces
{
    /// <summary>
    ///  Defines the application manager interface.
    /// </summary>
    public interface IApplicationBusinessManager
    {
        /// <summary>
        /// Gets the toggles asynchronous.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <returns></returns>
        Task<Response<IEnumerable<ToggleServiceConfigDto>>> GetTogglesAsync(string applicationId);

        /// <summary>
        /// Gets the application asynchronous.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <returns></returns>
        Task<Response<ApplicationDto>> GetApplicationAsync(string applicationId);

        /// <summary>
        /// Gets the applications asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<Response<IEnumerable<ApplicationDto>>> GetApplicationsAsync();
    }
}