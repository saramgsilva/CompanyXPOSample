
using System.Data.Entity;
using CompanyXPTO.ToggleService.DataAccess.Data;
using CompanyXPTO.ToggleService.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyXPTO.ToggleService.Platform.Tests.DataAccess
{
    [TestClass]
    public class SeedConfigTest
    {
        [TestMethod]
        public void DefineApplications()
        {
            var apps = SeedConfig.DefineApplications();
            ApplicationTest.AssertApplications(apps);
        }

        [TestMethod]
        public void DefineToggles()
        {
            var toggles = SeedConfig.DefineToggles(SeedConfig.DefineApplications());
            ToggleTest.AssertToggles(toggles);
        }
    }
}
