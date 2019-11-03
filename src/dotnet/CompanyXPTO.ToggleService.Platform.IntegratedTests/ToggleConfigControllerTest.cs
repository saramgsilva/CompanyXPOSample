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
    public class ToggleConfigControllerTest
    {
        [TestMethod]
        public async Task GetTogglesAsync_ServiceWithoutToggleService_Ok()
        {
            using (var client = new HttpClient())
            {
                var id = "ec3d4bfc-7788-4568-a7a7-0e498e990099";
                var fullUrl = new UriBuilder(Path.Combine(Configs.ToggleServiceConfigBaseUrl, id)).ToString();

                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(fullUrl));
                using (var response = await client.SendAsync(request))
                {
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                    var responseData = await response.Content.ReadAsStringAsync();
                    Assert.IsFalse(string.IsNullOrEmpty(responseData));
                    Console.WriteLine(responseData);

                    var data = JsonConvert.DeserializeObject<Response<IEnumerable<ToggleServiceConfigDto>>>(responseData);

                    Assert.IsFalse(data.IsValid);
                    Assert.AreEqual(data.ErrorCode,"102");
                    Assert.IsNull(data.Result);
                    Assert.IsNotNull(data.Message);
                }
            }
        }

        /// <summary>
        /// Gets the toggles asynchronous ok from ABC company.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetTogglesAsync_ABC_Ok()
        {
            using (var client = new HttpClient())
            {
                var id = "f7456d63-0583-4307-ad0e-b244abbcd8c5";
                var fullUrl = new UriBuilder(Path.Combine(Configs.ToggleServiceConfigBaseUrl, id)).ToString();

                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(fullUrl));
                using (var response = await client.SendAsync(request))
                {
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                    var responseData = await response.Content.ReadAsStringAsync();
                    Assert.IsFalse(string.IsNullOrEmpty(responseData));
                    Console.WriteLine(responseData);

                    var data = JsonConvert.DeserializeObject<Response<IEnumerable<ToggleServiceConfigDto>>>(responseData);

                    Assert.IsTrue(data.IsValid);
                    Assert.IsNotNull(data.Result);

                    // isButtonBlue 
                    // Assert.IsTrue(data.Result.Count() == 1);
                }
            }
        }

        /// <summary>
        /// Gets the toggles asynchronous ok from ABC company.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetTogglesAsync_A_Ok()
        {
            using (var client = new HttpClient())
            {
                var id = "2f8f037f-b1d6-4851-b0c6-f13773d1eed3";
                var fullUrl = new UriBuilder(Path.Combine(Configs.ToggleServiceConfigBaseUrl, id)).ToString();

                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(fullUrl));
                using (var response = await client.SendAsync(request))
                {
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                    var responseData = await response.Content.ReadAsStringAsync();
                    Assert.IsFalse(string.IsNullOrEmpty(responseData));
                    Console.WriteLine(responseData);

                    var data = JsonConvert.DeserializeObject<Response<IEnumerable<ToggleServiceConfigDto>>>(responseData);

                    Assert.IsTrue(data.IsValid);
                    Assert.IsNotNull(data.Result);

                    // sButtonBlue 
                    // isButtonGreen 
                    // isButtonBlue 
                    // Assert.IsTrue(data.Result.Count() == 3);
                }
            }
        }

        /// <summary>
        /// Gets the toggles asynchronous ok from ABC company.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetTogglesAsync_XPTO_Ok()
        {
            using (var client = new HttpClient())
            {
                var id = "1de8cb5d-72ab-4ef4-be90-c1baefbdd732";
                var fullUrl = new UriBuilder(Path.Combine(Configs.ToggleServiceConfigBaseUrl, id)).ToString();

                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(fullUrl));
                using (var response = await client.SendAsync(request))
                {
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                    var responseData = await response.Content.ReadAsStringAsync();
                    Assert.IsFalse(string.IsNullOrEmpty(responseData));
                    Console.WriteLine(responseData);

                    var data = JsonConvert.DeserializeObject<Response<IEnumerable<ToggleServiceConfigDto>>>(responseData);

                    Assert.IsTrue(data.IsValid);
                    Assert.IsNotNull(data.Result);

                    // sButtonBlue 
                    // isButtonBlue 
                    // Assert.IsTrue(data.Result.Count() == 2);
                }
            }
        }

        /// <summary>
        /// Gets the toggles asynchronous not found without application identifier.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetTogglesAsync_NotFound_WithoutApplicationId()
        {
            using (var client = new HttpClient())
            {
                var url = "toggles";
                var fullUrl = new UriBuilder(Path.Combine(Configs.ToggleServiceConfigBaseUrl, url)).ToString();

                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(fullUrl));
                using (var response = await client.SendAsync(request))
                {
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
                }
            }
        }

        /// <summary>
        /// Gets the toggles asynchronous not found with application identifier not exists.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetTogglesAsync_NotFound_WithApplicationIdNotExists()
        {
            using (var client = new HttpClient())
            {
                var id = "4d8cb999-2055-4d80-81f8-661e7eefbb77";
                var fullUrl = new UriBuilder(Path.Combine(Configs.ToggleServiceConfigBaseUrl, id)).ToString();

                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(fullUrl));
                using (var response = await client.SendAsync(request))
                {
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
                }
            }
        }
    }
}
