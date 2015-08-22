using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class RateLimitEndpointTests
    {
        private const string RateLimitResponse =
            "{\"data\":{ \"UserLimit\":412, \"UserRemaining\":382, \"UserReset\":1439945895, \"ClientLimit\":10500, \"ClientRemaining\":9500 }, \"success\":true, \"status\":200 }";

        [TestMethod]
        public void RateLimitEndpoint_GetRateLimitAsync_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IRateLimitEndpoint>();
            endpoint.GetRateLimitAsync();
            endpoint.Received().GetRateLimitAsync();
        }

        [TestMethod]
        public void ProcessRateLimitEndpointResponse_WithSuccessfulResponse_AreEqual()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);
            var rateLimit = endpoint.ProcessEndpointResponse<RateLimit>(RateLimitResponse);

            Assert.AreEqual(412, rateLimit.UserLimit);
            Assert.AreEqual(382, rateLimit.UserRemaining);
            Assert.AreEqual(10500, rateLimit.ClientLimit);
            Assert.AreEqual(9500, rateLimit.ClientRemaining);
        }
    }
}