using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Authentication
{
    [TestClass]
    public class OAuth2EndpointTests : TestBase
    {
        [TestMethod]
        public async Task GetTokenByRefreshToken_SetToken_IsNotNull()
        {
            var authentication = new ImgurAuthentication(ClientId, ClientSecret);
            var endpoint = new OAuth2Endpoint(authentication);
            var token = await endpoint.GetTokenByRefreshToken(RefreshToken);

            Assert.IsNotNull(token);
            Assert.IsFalse(string.IsNullOrWhiteSpace(token.AccessToken));
            Assert.IsFalse(string.IsNullOrWhiteSpace(token.RefreshToken));
            Assert.IsFalse(string.IsNullOrWhiteSpace(token.AccountId));
            Assert.IsFalse(string.IsNullOrWhiteSpace(token.TokenType));
        }

        [TestMethod]
        [ExpectedException(typeof(ImgurException))]
        public async Task GetTokenByCode_SetCodeInvalid_ThrowsAPIResponseException()
        {
            var authentication = new ImgurAuthentication(ClientId, ClientSecret);
            var endpoint = new OAuth2Endpoint(authentication);
            await endpoint.GetTokenByCode("abc");
        }
    }
}