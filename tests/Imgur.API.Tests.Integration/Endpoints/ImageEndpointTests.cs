using System.IO;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints
{
    [TestClass]
    public class ImageEndpointTests : TestBase
    {
        [TestMethod]
        public async Task UploadImageBinaryAsync_GetRateLimitWithImgurAuthentication_AreEqual()
        {
            var apiAuthentication = new MashapeAuthentication(ClientId, ClientSecret, MashapeKey);
            var oAuthEndpoint = new OAuth2Endpoint(apiAuthentication);
            var oAuth2Token = await oAuthEndpoint.GetTokenByRefreshTokenAsync(RefreshToken);

            apiAuthentication.SetOAuth2Token(oAuth2Token);

            var endpoint = new ImageEndpoint(apiAuthentication);
            var file = File.ReadAllBytes("banana.gif");
            var image = await endpoint.UploadImageBinaryAsync(file, null, "binary test title!", "binary test desc!");

            Assert.IsFalse(string.IsNullOrEmpty(image.Id));
            Assert.IsFalse(string.IsNullOrEmpty(image.AccountId));
            Assert.AreEqual("binary test title!", image.Title);
            Assert.AreEqual("binary test desc!", image.Description);
        }
    }
}