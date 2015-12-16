using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints
{
    [TestClass]
    public class OAuth2EndpointTests : TestBase
    {
        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public async Task GetTokenByCodeAsync_SetCodeInvalid_ThrowsImgurException()
        {
            var authentication = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new OAuth2Endpoint(authentication);
            await endpoint.GetTokenByCodeAsync("abc");
        }

        [TestMethod]
        public async Task GetTokenByRefreshTokenAsync_SetToken_IsNotNull()
        {
            var authentication = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new OAuth2Endpoint(authentication);
            var token = await endpoint.GetTokenByRefreshTokenAsync(RefreshToken);

            Assert.IsNotNull(token);
            Assert.IsFalse(string.IsNullOrWhiteSpace(token.AccessToken));
            Assert.IsFalse(string.IsNullOrWhiteSpace(token.RefreshToken));
            Assert.IsFalse(string.IsNullOrWhiteSpace(token.AccountId));
            Assert.IsFalse(string.IsNullOrWhiteSpace(token.TokenType));
        }
    }
}