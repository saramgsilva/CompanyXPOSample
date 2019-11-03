using System.Data.Entity;
using CompanyXPTO.ToggleService.Core.Interfaces;
using CompanyXPTO.ToggleService.DataAccess;
using CompanyXPTO.ToggleService.DataAccess.Repositories;
using CompanyXPTO.ToggleService.DataAccess.Repositories.Interfaces;
using SimpleInjector;

namespace CompanyXPTO.ToggleService.Core
{
    public static class ContainerManager
    {
        public static void RegisterDependencies(this Container container)
        {
            container.Register<DbContext, ToogleServiceContext>(Lifestyle.Scoped);
            container.Register<IRepositoryFactory, RepositoryFactory>(Lifestyle.Scoped);
            container.Register<IUnitOfWork<DbContext>, UnitOfWork<DbContext>>(Lifestyle.Scoped);
            container.Register<IToggleBusinessManager, ToggleBusinessManager>(Lifestyle.Scoped);
            container.Register<IApplicationBusinessManager, ApplicationBusinessManager>(Lifestyle.Scoped);
        }
    }
}