using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Fakes
{
    [TestClass]
    public class FakeHttpMessageHandlerTests
    {
        [TestMethod]
        public async Task HttpClient_GetAsync_ResponseContent_IsNull()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent("hello world")};

            fakeHttpMessageHandler.AddFakeResponse(new Uri("http://example.org/exists"), fakeResponse);

            var httpClient = new HttpClient(fakeHttpMessageHandler);
            var httpResponse = await httpClient.GetAsync("http://example.org/notfound");

            Assert.IsNull(httpResponse.Content);
        }

        [TestMethod]
        public async Task HttpClient_GetAsync_ResponseContentAreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent("get response")};

            fakeHttpMessageHandler.AddFakeResponse(new Uri("http://example.org/test"), fakeResponse);

            var httpClient = new HttpClient(fakeHttpMessageHandler);
            var httpResponse = await httpClient.GetAsync("http://example.org/test");
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            Assert.AreEqual("get response", stringResponse);
        }

        [TestMethod]
        public async Task HttpClient_PostAsync_ResponseContentAreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent("post response")};

            fakeHttpMessageHandler.AddFakeResponse(new Uri("http://example.org/test"), fakeResponse);

            var httpClient = new HttpClient(fakeHttpMessageHandler);
            var parameters = new Dictionary<string, string> {{"Name", "bob"}};

            var content = new FormUrlEncodedContent(parameters.ToArray());

            var httpResponse = await httpClient.PostAsync("http://example.org/test", content);
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            Assert.AreEqual("post response", stringResponse);
        }
    }
}