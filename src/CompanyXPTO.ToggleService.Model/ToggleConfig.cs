using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyXPTO.ToggleService.Model
{
    /// <summary>
    /// Defines the toggle configs entity, which is used to match toggle with applications and its value.
    /// </summary>
    /// <seealso cref="CompanyXPTO.ToggleService.Model.EntityBase" />
    public class ToggleConfig : EntityBase
    {
        /// <summary>
        /// Gets or sets the toggle identifier.
        /// </summary>
        /// <value>
        /// The toggle identifier.
        /// </value>
        public string ToggleId { get; set; }

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        /// <value>
        /// The application.
        /// </value>
        [ForeignKey(nameof(ApplicationId))]
        public Application Application { get; set; }

        /// <summary>
        /// Gets or sets the toggle.
        /// </summary>
        /// <value>
        /// The toggle.
        /// </value>
        [ForeignKey(nameof(ToggleId))]
        public Toggle Toggle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ToggleConfig"/> is value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if value; otherwise, <c>false</c>.
        /// </value>
        public bool Value { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }
    }
}