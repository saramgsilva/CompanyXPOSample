using System.Collections.Generic;

namespace CompanyXPTO.ToggleService.Dtos
{
    public class ToggleDto : ToggleServiceConfigDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the applications.
        /// </summary>
        /// <value>
        /// The applications.
        /// </value>
        public IEnumerable<ApplicationDto> Applications { get; set; }
    }
}
