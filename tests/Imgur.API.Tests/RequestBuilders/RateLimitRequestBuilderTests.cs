using System;
using System.Net.Http;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.RequestBuilders
{
    [TestClass]
    public class RateLimitRequestBuilderTests
    {
        [TestMethod]
        public void GetRateLimitRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new RateLimitEndpoint(imgurClient);
            var url = $"{endpoint.GetEndpointBaseUrl()}credits";
            var request = endpoint.RequestBuilder.GetRateLimitRequest(url);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/credits", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Get, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetRateLimitRequest_WithNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new RateLimitEndpoint(imgurClient);
            endpoint.RequestBuilder.GetRateLimitRequest(null);
        }
    }
}