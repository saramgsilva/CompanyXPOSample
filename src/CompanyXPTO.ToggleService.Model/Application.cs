
using System.Collections.Generic;

namespace CompanyXPTO.ToggleService.Model
{
    /// <summary>
    /// Defines the application entity.
    /// </summary>
    /// <seealso cref="CompanyXPTO.ToggleService.Model.EntityBase" />
    public class Application : EntityBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is toggle service allowed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is toggle service allowed; otherwise, <c>false</c>.
        /// </value>
        public bool IsToggleServiceAllowed { get; set; }

        /// <summary>
        /// Gets or sets the configs.
        /// </summary>
        /// <value>
        /// The configs.
        /// </value>
        public virtual ICollection<ToggleConfig> Configs { get; set; }
    }
}