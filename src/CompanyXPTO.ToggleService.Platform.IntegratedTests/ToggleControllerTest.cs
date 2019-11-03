using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CompanyXPTO.ToggleService.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CompanyXPTO.ToggleService.Platform.IntegratedTests
{
    [TestClass]
    public class ToggleControllerTest
    {
        /// <summary>
        /// Gets the toggles asynchronous ok.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetTogglesAsync_Ok()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(Configs.ToggleBaseUrl));
                using (var response = await client.SendAsync(request))
                {
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                    var responseData = await response.Content.ReadAsStringAsync();
                    Assert.IsFalse(string.IsNullOrEmpty(responseData));
                    Console.WriteLine(responseData);

                    var data = JsonConvert.DeserializeObject<Response<IEnumerable<ToggleDto>>>(responseData);
                    
                    Assert.IsTrue(data.IsValid);
                    Assert.IsNotNull(data.Result);
                    foreach (var item in data.Result)
                    {
                        Assert.IsNotNull(item.Id);
                        Assert.IsNotNull(item.Name);
                        foreach (var app in item.Applications)
                        {
                            Assert.IsNotNull(app.Id);
                            Assert.IsNotNull(app.Version);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the toggle asynchronous abc ok.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetToggleAsync_isButtonRed_Ok()
        {
            using (var client = new HttpClient())
            {
                var id = "8d2629eb-b69d-4f97-88e4-8d3e39d57197";
                var fullUrl = new UriBuilder(Path.Combine(Configs.ToggleBaseUrl, id)).ToString();

                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(fullUrl));
                using (var response = await client.SendAsync(request))
                {
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                    var responseData = await response.Content.ReadAsStringAsync();
                    Assert.IsFalse(string.IsNullOrEmpty(responseData));
                    Console.WriteLine(responseData);

                    var data = JsonConvert.DeserializeObject<Response<ToggleDto>>(responseData);

                    Assert.IsTrue(data.IsValid);
                    Assert.IsNotNull(data.Result);
                    Assert.IsTrue(data.Result.Name.Equals("isButtonRed"));
                }
            }
        }

        /// <summary>
        /// Creates the toggle ok.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CreateToggle_Ok()
        {
            var toggle = DefineToggleDto();

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, new Uri(Configs.ToggleBaseUrl));
                var body = JsonConvert.SerializeObject(toggle);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                using (var response = await client.SendAsync(request))
                {
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                    var responseData = await response.Content.ReadAsStringAsync();
                    Assert.IsFalse(string.IsNullOrEmpty(responseData));
                    Console.WriteLine(responseData);

                    var data = JsonConvert.DeserializeObject<Response<ToggleDto>>(responseData);

                    Assert.IsTrue(data.IsValid);
                    Assert.IsNotNull(data.Result);
                }
            }
        }

        /// <summary>
        /// Defines the toggle dto.
        /// </summary>
        /// <returns></returns>
        private static ToggleDto DefineToggleDto()
        {
            var id = Guid.NewGuid().ToString();
            var toggle = new ToggleDto
            {
                Id = id,
                Name = $"isNewToggle {id}",
                Version = "v1",
                Applications = new List<ApplicationDto>
                {
                    new ApplicationDto
                    {
                        Name = "XPTO",
                        Id = "1de8cb5d-72ab-4ef4-be90-c1baefbdd732"
                    },

                    new ApplicationDto
                    {
                        Name = "ABC",
                        Id = "f7456d63-0583-4307-ad0e-b244abbcd8c5"
                    }
                }
            };
            return toggle;
        }

        /// <summary>
        /// Deletes the toggle asynchronous toggle not found.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task DeleteToggleAsync_NotFound_WithToggleNotExists()
        {
            using (var client = new HttpClient())
            {
                var id = "4d8cb999-2055-4d80-81f8-661e7eefbb77";
                var fullUrl = new UriBuilder(Path.Combine(Configs.ToggleBaseUrl, id)).ToString();

                var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(fullUrl));
                using (var response = await client.SendAsync(request))
                {
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
                }
            }
        }

        /// <summary>
        /// Deletes the toggle asynchronous ok.
        /// </summary>
        /// <returns></returns>
        [Ignore]
        [TestMethod]
        public async Task DeleteToggleAsync_Ok()
        {
            using (var client = new HttpClient())
            {
                var id = "114eafbe-196d-4709-a10a-f402db2f739f";
                var fullUrl = new UriBuilder(Path.Combine(Configs.ToggleBaseUrl, id)).ToString();

                var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(fullUrl));
                using (var response = await client.SendAsync(request))
                {
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                }
            }
        }
    }
}