using System;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

namespace CompanyXPTO.ToggleService.Platform
{
    /// <summary>
    ///  Defines the migrations configuration.
    /// </summary>
    public sealed class MigrationConfig
    {
        /// <summary>
        /// Runs the migrations asynchronous.
        /// </summary>
        /// <returns>The result from migration process.</returns>
        public static async Task<string> RunMigrationsAsync()
        {
            try
            {
                var migrator = new DbMigrator(new DataAccess.Migrations.Configuration());
                migrator.Update();

                return await Task.FromResult("Ok");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}