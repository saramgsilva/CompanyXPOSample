
using System.Collections.Generic;

namespace CompanyXPTO.ToggleService.Model
{
    /// <summary>
    /// Defines the toggle entity.
    /// </summary>
    /// <seealso cref="CompanyXPTO.ToggleService.Model.EntityBase" />
    public class Toggle : EntityBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the configs.
        /// </summary>
        /// <value>
        /// The configs.
        /// </value>
        public virtual ICollection<ToggleConfig> Configs { get; set; }
    }
}
