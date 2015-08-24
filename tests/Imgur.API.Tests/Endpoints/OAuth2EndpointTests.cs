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
        private const string OAuth2TokenResponseError = "{\"data\":{\"error\":\"Refresh token doesn't exist or is invalid for the client\",\"request\":\"\\/oauth2\\/token\",\"method\":\"POST\"},\"success\":false,\"status\":400}";
        private const string OAuth2TokenResponse =
            "{\"access_token\":\"20649dae013aiuiui87878788787975ae2\",\"expires_in\":3600,\"token_type\":\"bearer\",\"scope\":null,\"refresh_token\":\"2132d34234jkljj84ce0c16fjkljfsdfdc70\",\"account_id\":45344,\"account_username\":\"Bob\"}";

        [TestMethod]
        public void OAuth2Endpoint_GetAuthorizationUrl_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IOAuth2Endpoint>();
            endpoint.GetAuthorizationUrl(OAuth2ResponseType.Code, null);
            endpoint.Received().GetAuthorizationUrl(OAuth2ResponseType.Code, null);
        }

        [TestMethod]
        public void OAuth2Endpoint_GetTokenByPinAsync_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IOAuth2Endpoint>();
            endpoint.GetTokenByPinAsync("1234");
            endpoint.Received().GetTokenByPinAsync("1234");
        }

        [TestMethod]
        public void OAuth2Endpoint_GetTokenByCodeAsync_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IOAuth2Endpoint>();
            endpoint.GetTokenByCodeAsync("1234");
            endpoint.Received().GetTokenByCodeAsync("1234");
        }

        [TestMethod]
        public void OAuth2Endpoint_GetTokenByRefreshTokenAsync_ReceivedIsTrue()
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
            var endPoint = new OAuth2Endpoint(imgurAuthentication);
            Assert.AreEqual(endpointUrl, endPoint.GetAuthorizationUrl(OAuth2ResponseType.Code, null));
        }

        [TestMethod]
        public void GetAuthorizationUrl_SetState_AreEqual()
        {
            var endpointUrl = "https://api.imgur.com/oauth2/authorize?client_id=ClientId&response_type=Code&state=test";
            var imgurAuthentication = new ImgurAuthentication("ClientId", "ClientSecret");
            var endPoint = new OAuth2Endpoint(imgurAuthentication);
            Assert.AreEqual(endpointUrl, endPoint.GetAuthorizationUrl(OAuth2ResponseType.Code, "test"));
        }

        [TestMethod]
        [ExpectedException(typeof(ImgurException))]
        public void OAuth2Token_ProcessEndpointResponse_ThrowsNewImgurException()
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
            var token = endpoint.ProcessEndpointResponse<OAuth2Token>(OAuth2TokenResponse);

            Assert.AreEqual("20649dae013aiuiui87878788787975ae2", token.AccessToken);
            Assert.AreEqual("45344", token.AccountId);
            Assert.AreEqual("2132d34234jkljj84ce0c16fjkljfsdfdc70", token.RefreshToken);
            Assert.AreEqual("bearer", token.TokenType);
            Assert.AreEqual(3600, token.ExpiresIn);
        }
    }
}