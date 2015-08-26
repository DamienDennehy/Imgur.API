using System;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Exceptions;
using Imgur.API.Models.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class OAuth2EndpointTests
    {
        private const string OAuth2TokenResponseError =
            "{\"data\":{\"error\":\"Refresh token doesn't exist or is invalid for the client\",\"request\":\"\\/oauth2\\/token\",\"method\":\"POST\"},\"success\":false,\"status\":400}";

        private const string OAuth2TokenResponse =
            "{\"access_token\":\"20649dae013aiuiui87878788787975ae2\",\"expires_in\":3600,\"token_type\":\"bearer\",\"scope\":null,\"refresh_token\":\"2132d34234jkljj84ce0c16fjkljfsdfdc70\",\"account_id\":45344,\"account_username\":\"Bob\"}";

        [TestMethod]
        public void GetAuthorizationUrl_WithResponseType_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IOAuth2Endpoint>();
            endpoint.GetAuthorizationUrl(OAuth2ResponseType.Code, null);
            endpoint.Received().GetAuthorizationUrl(OAuth2ResponseType.Code, null);
        }

        [TestMethod]
        public void GetTokenByPinAsync_WithPin_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IOAuth2Endpoint>();
            endpoint.GetTokenByPinAsync("1234");
            endpoint.Received().GetTokenByPinAsync("1234");
        }

        [TestMethod]
        public void GetTokenByCodeAsync_WithCode_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IOAuth2Endpoint>();
            endpoint.GetTokenByCodeAsync("1234");
            endpoint.Received().GetTokenByCodeAsync("1234");
        }

        [TestMethod]
        public void GetTokenByRefreshTokenAsync_WithRefreshToken_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IOAuth2Endpoint>();
            endpoint.GetTokenByRefreshTokenAsync("1234");
            endpoint.Received().GetTokenByRefreshTokenAsync("1234");
        }

        [TestMethod]
        public void GetAuthorizationUrl_SetStateNull_AreEqual()
        {
            var endpointUrl = "https://api.imgur.com/oauth2/authorize?client_id=ClientId&response_type=Code&state=";
            var imgurAuthentication = new ImgurAuthentication("ClientId", "ClientSecret");
            var endpoint = new OAuth2Endpoint(imgurAuthentication);
            Assert.AreEqual(endpointUrl, endpoint.GetAuthorizationUrl(OAuth2ResponseType.Code, null));
        }

        [TestMethod]
        public void GetAuthorizationUrl_SetState_AreEqual()
        {
            var endpointUrl = "https://api.imgur.com/oauth2/authorize?client_id=ClientId&response_type=Code&state=test";
            var imgurAuthentication = new ImgurAuthentication("ClientId", "ClientSecret");
            var endpoint = new OAuth2Endpoint(imgurAuthentication);
            Assert.AreEqual(endpointUrl, endpoint.GetAuthorizationUrl(OAuth2ResponseType.Code, "test"));
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetTokenByCodeAsync_WithNull_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurAuth);
            await endpoint.GetTokenByCodeAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetTokenByPinAsync_WithNull_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurAuth);
            await endpoint.GetTokenByPinAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetTokenByRefreshTokenAsync_WithNull_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurAuth);
            await endpoint.GetTokenByRefreshTokenAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public void OAuth2Token_ProcessEndpointResponse_ThrowsImgurException()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);
            endpoint.ProcessEndpointResponse<OAuth2Token>(OAuth2TokenResponseError);
        }

        [TestMethod]
        public void OAuth2Token_ProcessEndpointResponse_AreEqual()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);
            var oAuth2Token = endpoint.ProcessEndpointResponse<OAuth2Token>(OAuth2TokenResponse);

            Assert.AreEqual("20649dae013aiuiui87878788787975ae2", oAuth2Token.AccessToken);
            Assert.AreEqual("45344", oAuth2Token.AccountId);
            Assert.AreEqual("2132d34234jkljj84ce0c16fjkljfsdfdc70", oAuth2Token.RefreshToken);
            Assert.AreEqual("bearer", oAuth2Token.TokenType);
            Assert.AreEqual(3600, oAuth2Token.ExpiresIn);
        }
    }
}