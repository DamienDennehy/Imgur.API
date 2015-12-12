using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Tests.FakeResponses;
using Imgur.API.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class RateLimitEndpointTests
    {
        [TestMethod]
        public async Task GetRateLimitAsync_AreEqual()
        {
            //Create a fake message handler
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(RateLimitEndpointResponses.RateLimitResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/credits"), fakeResponse);

            //Create a HttpClient that will use the fake handler
            var httpClient = new HttpClient(fakeHttpMessageHandler);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new RateLimitEndpoint(imgurClient, httpClient);

            var rateLimit = await endpoint.GetRateLimitAsync();

            Assert.IsNotNull(rateLimit);
            Assert.AreEqual(412, rateLimit.UserLimit);
            Assert.AreEqual(382, rateLimit.UserRemaining);
            Assert.AreEqual(10500, rateLimit.ClientLimit);
            Assert.AreEqual(9500, rateLimit.ClientRemaining);
        }
    }
}