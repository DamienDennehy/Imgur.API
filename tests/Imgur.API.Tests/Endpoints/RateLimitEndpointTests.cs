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
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(RateLimitEndpointResponses.RateLimitResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/credits"), fakeResponse);

            var httpClient = new HttpClient(fakeHttpMessageHandler);

            var client = new ImgurClient("123", "1234");
            var endpoint = new RateLimitEndpoint(client, httpClient);

            var rateLimit = await endpoint.GetRateLimitAsync();

            Assert.IsNotNull(rateLimit);
            Assert.AreEqual(10500, rateLimit.ClientLimit);
            Assert.AreEqual(9500, rateLimit.ClientRemaining);
        }
    }
}