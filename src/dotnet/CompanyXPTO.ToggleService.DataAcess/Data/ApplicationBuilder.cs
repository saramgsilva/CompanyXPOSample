using System;
using CompanyXPTO.ToggleService.Model;

namespace CompanyXPTO.ToggleService.DataAccess.Data
{
    /// <summary>
    /// Defines the application entity builder-
    /// </summary>
    public sealed class ApplicationBuilder
    {
        private readonly Application _application;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationBuilder"/> class.
        /// </summary>
        public ApplicationBuilder()
        {
            _application = new Application
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                IsToggleServiceAllowed = true
            };
        }

        /// <summary>
        /// Withes the identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ApplicationBuilder WithId(string id)
        {
            _application.Id = id;
            return this;
        }

        /// <summary>
        /// Withes the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The ApplicationBuilder object.</returns>
        public ApplicationBuilder WithName(string name)
        {
            _application.Name = name;
            return this;
        }

        /// <summary>
        /// Alloweds the toggle service.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>The application builder.</returns>
        public ApplicationBuilder AllowedToggleService(bool value)
        {
            _application.IsToggleServiceAllowed = false;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>The Application object.</returns>
        public Application Build()
        {
            return _application;
        }
    }
}