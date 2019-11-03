using System.Data.Entity;
using System.Web.Http;
using CompanyXPTO.ToggleService.Core;
using CompanyXPTO.ToggleService.Core.Interfaces;
using CompanyXPTO.ToggleService.DataAccess;
using CompanyXPTO.ToggleService.DataAccess.Repositories;
using CompanyXPTO.ToggleService.DataAccess.Repositories.Interfaces;
using Newtonsoft.Json;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace CompanyXPTO.ToggleService.Platform
{
    /// <summary>
    ///  Defines the ioc configuration.
    /// </summary>
    public static class IocConfig
    {
        /// <summary>
        /// Initializes the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="container">The container.</param>
        public static void Initialize(HttpConfiguration config, Container container)
        {
            // Use this class to set configuration options for your mobile service
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.RegisterDependencies();
            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            jsonSettings.NullValueHandling = NullValueHandling.Ignore;
            jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            config.Formatters.JsonFormatter.SerializerSettings = jsonSettings;
        }
    }
}