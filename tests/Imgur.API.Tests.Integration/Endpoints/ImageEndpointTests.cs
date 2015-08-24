using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
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
            var token = await oAuthEndpoint.GetTokenByRefreshTokenAsync(RefreshToken);

            apiAuthentication.SetOAuth2Authentication(new OAuth2Authentication(token));

            var endpoint = new ImageEndpoint(apiAuthentication);
            var file = System.IO.File.ReadAllBytes("banana.gif");
            var image = await endpoint.UploadImageBinaryAsync(file, null, "binary test title!", "binary test desc!");

            Assert.IsFalse(string.IsNullOrEmpty(image.Id));
            Assert.IsFalse(string.IsNullOrEmpty(image.AccountId));
            Assert.AreEqual("binary test title!", image.Title);
            Assert.AreEqual("binary test desc!", image.Description);
        }
    }
}
