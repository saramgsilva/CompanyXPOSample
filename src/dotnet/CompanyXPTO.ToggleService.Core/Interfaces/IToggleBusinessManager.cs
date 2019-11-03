using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyXPTO.ToggleService.Dtos;

namespace CompanyXPTO.ToggleService.Core.Interfaces
{
    /// <summary>
    /// Define the toggle business manager interface.
    /// </summary>
    public interface IToggleBusinessManager
    {
        /// <summary>
        /// Gets the toggle asynchronous.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        Task<Response<ToggleDto>> GetToggleAsync(string Id);

        /// <summary>
        /// Gets the toggles asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<Response<IEnumerable<ToggleDto>>> GetTogglesAsync();

        /// <summary>
        /// Posts a toggle asynchronous.
        /// </summary>
        /// <param name="toogDto">The toog dto.</param>
        /// <returns></returns>
        Task<Response<ToggleDto>> PostToggleAsync(ToggleDto toogDto);

        /// <summary>
        /// Puts a toggle asynchronous.
        /// </summary>
        /// <param name="toogDto">The toog dto.</param>
        /// <returns></returns>
        Task<Response<ToggleDto>> PutToggleAsync(ToggleDto toogDto);

        /// <summary>
        /// Deletes the toggle.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteToggle(string id);
    }
}