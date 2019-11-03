using System;
using System.Collections.Generic;
using System.Linq;
using CompanyXPTO.ToggleService.Model;

namespace CompanyXPTO.ToggleService.DataAccess.Data
{
    /// <summary>
    /// Defines the Toogle entity builder.
    /// </summary>
    public sealed class ToggleBuilder
    {
        private readonly Toggle _toggle;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleBuilder" /> class.
        /// </summary>
        public ToggleBuilder()
        {
            _toggle = new Toggle
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
            };
        }

        /// <summary>
        /// Withes the identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">id</exception>
        public ToggleBuilder WithId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
            _toggle.Id = id;
            return this;
        }

        /// <summary>
        /// Withes the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The ToggleBuilder object.</returns>
        public ToggleBuilder WithName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            _toggle.Name = name;
            return this;
        }

        /// <summary>
        /// Alloweds the name of for application with.
        /// </summary>
        /// <param name="applicationName">The applicationName.</param>
        /// <param name="applications">The applications.</param>
        /// <returns>The ToggleBuilder object.</returns>
        public ToggleBuilder AllowedForApplicationWithName(string applicationName, IEnumerable<Application> applications)
        {
            if (string.IsNullOrEmpty(applicationName))
            {
                throw new ArgumentNullException(nameof(applicationName));
            }
            if (applications == null)
            {
                throw new ArgumentNullException(nameof(applications));
            }
            AllowedForAllApplications(applications.Where(a => a.Name == applicationName));
            return this;
        }

        /// <summary>
        /// Alloweds for all applications.
        /// </summary>
        /// <param name="applications">The applications.</param>
        /// <returns>The ToggleBuilder object.</returns>
        public ToggleBuilder AllowedForAllApplications(IEnumerable<Application> applications)
        {
            if (applications == null)
            {
                throw new ArgumentNullException(nameof(applications));
            }
            foreach (var app in applications)
            {
                var toggleConfig = new ToggleConfig
                {
                    Id = Guid.NewGuid().ToString(),
                    Version = "v1",
                    ApplicationId = app.Id,
                    ToggleId = _toggle.Id,
                    Application = app,
                    Toggle = _toggle,
                    Value = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                if (_toggle.Configs == null)
                {
                    _toggle.Configs = new List<ToggleConfig>();
                }
                if (app.Configs == null)
                {
                    app.Configs = new List<ToggleConfig>();
                }
                _toggle.Configs.Add(toggleConfig);
                app.Configs.Add(toggleConfig);
            }
            return this;
        }

        /// <summary>
        /// Excepts the name of the application with.
        /// </summary>
        /// <param name="applicationName">The applicationName.</param>
        /// <param name="applications">The applications.</param>
        /// <returns>The ToggleBuilder object.</returns>
        public ToggleBuilder ExceptApplicationWithName(string applicationName, IEnumerable<Application> applications)
        {
            if (string.IsNullOrEmpty(applicationName))
            {
                throw new ArgumentNullException(nameof(applicationName));
            }
            if (applications == null)
            {
                throw new ArgumentNullException(nameof(applications));
            }
            AllowedForAllApplications(applications.Except(applications.Where(a => a.Name == applicationName)));
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>The Toggle object.</returns>
        public Toggle Build()
        {
            return _toggle;
        }
    }
}