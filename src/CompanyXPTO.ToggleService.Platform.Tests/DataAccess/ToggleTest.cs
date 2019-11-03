using System;
using System.Collections.Generic;
using CompanyXPTO.ToggleService.DataAccess.Data;
using CompanyXPTO.ToggleService.Model;
using CompanyXPTO.ToggleService.Platform.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyXPTO.ToggleService.Platform.Tests.DataAccess
{
    [TestClass]
    public class ToggleTest
    {
        [TestMethod]
        public void CreateToggleWithConfig_Ok()
        {
            var toggle = new ToggleBuilder().WithId("625c3b92-c94e-435a-84e5-66e02c346fb4").WithName("isButtonGreen").Build();
            Assert.IsNotNull(toggle.Name);
            Assert.IsNotNull(toggle.Id);
            Assert.IsNotNull(toggle.CreatedAt);
            Assert.IsNotNull(toggle.UpdateAt);
            Assert.IsNull(toggle.Configs);

            toggle.Configs = new List<ToggleConfig>
            {
                new ToggleConfig
                {
                    Id = "af898a6d-758c-40ef-b6ce-0ebc63df460c",
                    Version = "v1",
                    ApplicationId = "ece9a845-1898-485a-9ef2-0bafc7db5570",
                    ToggleId =toggle.Id 
                }
            };

            Assert.IsNotNull(toggle.Configs);
            foreach (var config in toggle.Configs)
            {
                Assert.IsNotNull(config.Version);
                Assert.IsNotNull(config.Id);
                Assert.IsNotNull(config.CreatedAt);
                Assert.IsNotNull(config.UpdateAt);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToggleBuilder_WithoutId()
        {
            new ToggleBuilder().WithId(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToggleBuilder_WithoutApplicationName()
        {
            new ToggleBuilder().AllowedForApplicationWithName(string.Empty, new List<Application>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToggleBuilder_WithoutAllowedApplications()
        {
            new ToggleBuilder().AllowedForApplicationWithName("abc", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToggleBuilder_WithoutName()
        {
            new ToggleBuilder().WithName(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToggleBuilder_WithoutAllowedForAllApplications()
        {
            new ToggleBuilder().AllowedForAllApplications(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToggleBuilder_WithoutNameApplication()
        {
            new ToggleBuilder().ExceptApplicationWithName(string.Empty,new List<Application>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToggleBuilder_WithoutApplications()
        {
            new ToggleBuilder().ExceptApplicationWithName("abc", null);
        }

        [TestMethod]
        public void GetToggles()
        {
            var toggles = ToggleFakeData.GetToggles();
            AssertToggles(toggles);
        }
        public static void AssertToggles(IEnumerable<Toggle> toggles)
        {
            foreach (var item in toggles)
            {
                Assert.IsNotNull(item.Name);
                Assert.IsNotNull(item.Id);
                Assert.IsNotNull(item.CreatedAt);
                Assert.IsNotNull(item.UpdateAt);
                Assert.IsNotNull(item.Configs);
                foreach (var config in item.Configs)
                {
                    Assert.IsNotNull(config.ToggleId);
                    Assert.IsNotNull(config.Toggle);
                    Assert.IsNotNull(config.Application);
                    Assert.IsNotNull(config.ApplicationId);
                    Assert.IsNotNull(config.Value);
                    Assert.IsNotNull(config.Id);
                    Assert.IsNotNull(config.CreatedAt);
                    Assert.IsNotNull(config.UpdateAt);
                }
            }
        }
    }
}