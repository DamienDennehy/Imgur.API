using Imgur.API.Endpoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class RateLimitEndpointTests
    {
        [TestMethod]
        public void RateLimitEndpoint_GetRateLimitAsync_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IRateLimitEndpoint>();
            endpoint.GetRateLimitAsync();
            endpoint.Received().GetRateLimitAsync();
        }
    }
}
