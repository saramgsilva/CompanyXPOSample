using System.Collections.Generic;
using CompanyXPTO.ToggleService.Model;
using CompanyXPTO.ToggleService.Platform.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyXPTO.ToggleService.Platform.Tests.DataAccess
{
    [TestClass]
    public class ApplicationTest
    {
        [TestMethod]
        public void GetApplications()
        {
            var apps = ApplicationFakeData.GetApplications();
            AssertApplications(apps);
        }
        
        public static void AssertApplications(IEnumerable<Application> apps)
        {
            foreach (var item in apps)
            {
                Assert.IsNotNull(item.Name);
                Assert.IsNotNull(item.Id);
                Assert.IsNotNull(item.CreatedAt);
                Assert.IsNotNull(item.UpdateAt);
                Assert.IsNull(item.Configs);}
        }
    }
}