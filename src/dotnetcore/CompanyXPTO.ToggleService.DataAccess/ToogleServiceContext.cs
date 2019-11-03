using System;
using CompanyXPTO.ToggleService.Model;
using Microsoft.EntityFrameworkCore;

namespace CompanyXPTO.ToggleService.DataAccess
{
    /// <summary>
    /// Defines the DbContext from the platform.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class ToogleServiceContext: DbContext
    {
        private const string ConnectionStringName = "DefaultConnection";

        public ToogleServiceContext(DbContextOptions<ToogleServiceContext> options)
        : base(options)
        { }

        /// <summary>
        /// Gets or sets the toggles.
        /// </summary>
        /// <value>
        /// The toggles.
        /// </value>
        public virtual DbSet<Toggle> Toggles { get; set; }

        /// <summary>
        /// Gets or sets the applications.
        /// </summary>
        /// <value>
        /// The applications.
        /// </value>
        public virtual DbSet<Application> Applications { get; set; }

        /// <summary>
        /// Gets or sets the toggle configs.
        /// </summary>
        /// <value>
        /// The toggle configs.
        /// </value>
        public virtual DbSet<ToggleConfig> ToggleConfigs { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <exception cref="ArgumentNullException">modelBuilder</exception>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }
            
            //modelBuilder.Entity<ToggleConfig>()
            //    .HasRequired<Toggle>(qtp => qtp.Toggle)
            //    .WithMany(p => p.Configs)
            //    .HasForeignKey(p => p.ToggleId)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<ToggleConfig>()
            //    .HasRequired<Application>(qtp => qtp.Application)
            //    .WithMany(p => p.Configs)
            //    .HasForeignKey(p => p.ApplicationId)
            //    .WillCascadeOnDelete(true);
        }
    }
}