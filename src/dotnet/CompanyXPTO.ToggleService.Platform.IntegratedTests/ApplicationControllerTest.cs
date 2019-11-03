using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CompanyXPTO.ToggleService.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CompanyXPTO.ToggleService.Platform.IntegratedTests
{
    [TestClass]
    public class ApplicationControllerTest
    {
       /// <summary>
        /// Gets the applications asynchronous ok.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetApplicationsAsync_Ok()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(Configs.ApplicationBaseUrl));
                using (var response = await client.SendAsync(request))
                {
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                    var responseData = await response.Content.ReadAsStringAsync();
                    Assert.IsFalse(string.IsNullOrEmpty(responseData));
                    Console.WriteLine(responseData);

                    var data = JsonConvert.DeserializeObject<Response<IEnumerable<ApplicationDto>>>(responseData);

                    Assert.IsTrue(data.IsValid);
                    Assert.IsNotNull(data.Result);
                    foreach (var item in data.Result)
                    {
                        Assert.IsNotNull(item.Id);
                        Assert.IsNotNull(item.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the application asynchronous abc ok.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetApplicationAsync_ABC_Ok()
        {
            using (var client = new HttpClient())
            {
                var id = "f7456d63-0583-4307-ad0e-b244abbcd8c5";
                var fullUrl = new UriBuilder(Path.Combine(Configs.ApplicationBaseUrl, id)).ToString();

                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(fullUrl));
                using (var response = await client.SendAsync(request))
                {
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                    var responseData = await response.Content.ReadAsStringAsync();
                    Assert.IsFalse(string.IsNullOrEmpty(responseData));
                    Console.WriteLine(responseData);

                    var data = JsonConvert.DeserializeObject<Response<ApplicationDto>>(responseData);

                    Assert.IsTrue(data.IsValid);
                    Assert.IsNotNull(data.Result);
                    Assert.IsTrue(data.Result.Name.Equals("ABC"));
                }
            }
        }
    }
}