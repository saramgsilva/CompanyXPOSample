using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyXPTO.ToggleService.Model
{
    /// <summary>
    ///  Define the entity base which will be used by all model entitites.
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the update at.
        /// </summary>
        /// <value>
        /// The update at.
        /// </value>
        public DateTime UpdateAt { get; set; }
    }
}