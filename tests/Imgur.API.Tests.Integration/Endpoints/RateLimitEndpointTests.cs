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
        public async Task RateLimit_GetRateLimitWithImgurAuthentication_IsValidRateLimit()
        {
            var authentication = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new RateLimitEndpoint(authentication);
            var limit = await endpoint.GetRateLimitAsync();
            Assert.IsNotNull(limit);
            Assert.IsTrue(limit.ClientLimit > 0);
            Assert.IsTrue(limit.ClientRemaining > 0);
            Assert.IsTrue(limit.UserLimit > 0);
            Assert.IsTrue(limit.UserRemaining > 0);
            Assert.IsNotNull(limit.UserReset);
            Assert.IsNull(limit.MashapeUploadsLimit);
            Assert.IsNull(limit.MashapeUploadsRemaining);
        }

        //[TestMethod]
        //public async Task RateLimit_GetRateLimitWithImgurAuthenticationAndOAuth2Authentication_IsValidRateLimit()
        //{
        //    var authentication = new ImgurClient(ClientId, ClientSecret, await GetOAuth2Token());

        //    var endpoint = new RateLimitEndpoint(authentication);
        //    var limit = await endpoint.GetRateLimitAsync();

        //    Assert.IsNotNull(limit);
        //    Assert.IsTrue(limit.ClientLimit > 0);
        //    Assert.IsTrue(limit.ClientRemaining > 0);
        //    Assert.IsTrue(limit.UserLimit > 0);
        //    Assert.IsTrue(limit.UserRemaining > 0);
        //    Assert.IsNotNull(limit.UserReset);
        //    Assert.IsNull(limit.MashapeUploadsLimit);
        //    Assert.IsNull(limit.MashapeUploadsRemaining);
        //}

        [TestMethod]
        public async Task RateLimit_GetRateLimitWithMashapeAuthentication_IsValidRateLimit()
        {
            var authentication = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new RateLimitEndpoint(authentication);
            var limit = await endpoint.GetRateLimitAsync();
            Assert.IsNotNull(limit);
            Assert.IsTrue(limit.ClientLimit > 0);
            Assert.IsTrue(limit.ClientRemaining > 0);
            Assert.IsTrue(limit.UserLimit > 0);
            Assert.IsTrue(limit.UserRemaining > 0);
            Assert.IsNotNull(limit.UserReset);
            Assert.IsNull(limit.MashapeUploadsLimit);
            Assert.IsNull(limit.MashapeUploadsRemaining);
        }

        //public async Task RateLimit_GetRateLimitWithMashapeAuthenticationAndOAuth2Authentication_IsValidRateLimit()
        //{
        //    var authentication = new ImgurClient(ClientId, ClientSecret, await GetOAuth2Token());

        //    var endpoint = new RateLimitEndpoint(authentication);
        //    var limit = await endpoint.GetRateLimitAsync();

        //    Assert.IsNotNull(limit);
        //    Assert.IsTrue(limit.ClientLimit > 0);
        //    Assert.IsTrue(limit.ClientRemaining > 0);
        //    Assert.IsTrue(limit.UserLimit > 0);
        //    Assert.IsTrue(limit.UserRemaining > 0);
        //    Assert.IsNotNull(limit.UserReset);
        //    Assert.IsNull(limit.MashapeUploadsLimit);
        //    Assert.IsNull(limit.MashapeUploadsRemaining);
        //}
    }
}