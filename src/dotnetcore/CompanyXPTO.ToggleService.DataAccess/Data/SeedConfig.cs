using System.Collections.Generic;
using System.Linq;
using CompanyXPTO.ToggleService.Model;
using Microsoft.EntityFrameworkCore;

namespace CompanyXPTO.ToggleService.DataAccess.Data
{
    /// <summary>
    /// Defines the entity that config initial data to fill the database,ie, dummy data.
    /// </summary>
    public static class SeedConfig
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static void Seed(DbContext context)
        {
            if (context.Set<Application>().Any())
            {
                return;
            }

            var applications = DefineApplications();
            var toggles = DefineToggles(applications);

            context.Set<Application>().AddRange(applications);
            context.Set<Toggle>().AddRange(toggles);

            foreach (var toggle in toggles)
            {
                context.Set<ToggleConfig>().AddRange(toggle.Configs);
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Defines the applications.
        /// </summary>
        /// <returns>The list of Application objects.</returns>
        public static IEnumerable<Application> DefineApplications()
        {
            return new List<Application>
            {
                new ApplicationBuilder().WithId("f7456d63-0583-4307-ad0e-b244abbcd8c5").WithName("ABC").Build(),
                new ApplicationBuilder().WithId("2f8f037f-b1d6-4851-b0c6-f13773d1eed3").WithName("A").Build(),
                new ApplicationBuilder().WithId("1de8cb5d-72ab-4ef4-be90-c1baefbdd732").WithName("XPTO").Build(),
                new ApplicationBuilder().WithId("ec3d4bfc-7788-4568-a7a7-0e498e990099").WithName("ServiceWithoutToggleService").AllowedToggleService(false).Build(),
            };
        }

        /// <summary>
        /// Defines the toggle.
        /// </summary>
        /// <param name="applications">The applications.</param>
        /// <returns>The list of Toggle objects.</returns>
        public static IEnumerable<Toggle> DefineToggles(IEnumerable<Application> applications)
        {
            return new List<Toggle>
            {
                new ToggleBuilder().WithId("114eafbe-196d-4709-a10a-f402db2f739f").WithName("isButtonBlue").AllowedForAllApplications(applications).Build(),
                new ToggleBuilder().WithId("5a0e5c11-650c-4758-a2dc-cd0fa923789e").WithName("isButtonGreen").AllowedForApplicationWithName("A",applications).Build(),
                new ToggleBuilder().WithId("8d2629eb-b69d-4f97-88e4-8d3e39d57197").WithName("isButtonRed").ExceptApplicationWithName("ABC", applications).Build(),
            };
        }
    }
}