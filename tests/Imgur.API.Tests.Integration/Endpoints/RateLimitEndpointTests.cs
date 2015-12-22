using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints
{
    [TestClass]
    public class RateLimitEndpointTests : TestBase
    {
        [TestMethod]
        public async Task RateLimit_GetRateLimitWithImgurClient_IsValidRateLimit()
        {
            var client = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new RateLimitEndpoint(client);
            var limit = await endpoint.GetRateLimitAsync();
            Assert.IsNotNull(limit);
            Assert.IsTrue(limit.ClientLimit > 0);
            Assert.IsTrue(limit.ClientRemaining > 0);
        }

        [TestMethod]
        public async Task RateLimit_GetRateLimitWithImgurClientAndOAuth2Authentication_IsValidRateLimit()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);

            var endpoint = new RateLimitEndpoint(client);
            var limit = await endpoint.GetRateLimitAsync();

            Assert.IsNotNull(limit);
            Assert.IsTrue(limit.ClientLimit > 0);
            Assert.IsTrue(limit.ClientRemaining > 0);
        }

        [TestMethod]
        public async Task RateLimit_GetRateLimitWithMashapeClient_IsValidRateLimit()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new RateLimitEndpoint(client);
            var limit = await endpoint.GetRateLimitAsync();
            Assert.IsNotNull(limit);
            Assert.IsTrue(limit.ClientLimit > 0);
            Assert.IsTrue(limit.ClientRemaining > 0);
        }

        [TestMethod]
        public async Task RateLimit_GetRateLimitWithMashapeClientAndOAuth2Authentication_IsValidRateLimit()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);

            var endpoint = new RateLimitEndpoint(client);
            var limit = await endpoint.GetRateLimitAsync();

            Assert.IsNotNull(limit);
            Assert.IsTrue(limit.ClientLimit > 0);
            Assert.IsTrue(limit.ClientRemaining > 0);
        }
    }
}