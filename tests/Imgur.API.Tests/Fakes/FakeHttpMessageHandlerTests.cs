using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Fakes
{
    [TestClass]
    public class FakeHttpMessageHandlerTests : TestBase
    {
        [TestMethod]
        public async Task HttpClient_GetAsync_ResponseContent_IsNull()
        {
            var fakeUrl = "http://example.org/exists";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent("hello world")};

            var httpClient = new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse));
            var httpResponse = await httpClient.GetAsync("http://example.org/notfound");

            Assert.IsNull(httpResponse.Content);
        }

        [TestMethod]
        public async Task HttpClient_GetAsync_ResponseContentAreEqual()
        {
            var fakeUrl = "http://example.org/test";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent("get response")};

            var httpClient = new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse));
            var httpResponse = await httpClient.GetAsync("http://example.org/test");
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            Assert.AreEqual("get response", stringResponse);
        }

        [TestMethod]
        public async Task HttpClient_PostAsync_ResponseContentAreEqual()
        {
            var fakeUrl = "http://example.org/test";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent("post response")};

            var httpClient = new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse));
            var parameters = new Dictionary<string, string> {{"Name", "bob"}};

            var content = new FormUrlEncodedContent(parameters.ToArray());

            var httpResponse = await httpClient.PostAsync("http://example.org/test", content);
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            Assert.AreEqual("post response", stringResponse);
        }
    }
}