using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Exceptions;
using Imgur.API.Tests.FakeResponses;
using Imgur.API.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class OAuth2EndpointTests
    {
        [TestMethod]
        public void GetAuthorizationUrl_SetState_AreEqual()
        {
            var imgurClient = new ImgurClient("abc", "ioa");
            var endpoint = new OAuth2Endpoint(imgurClient);
            var expected = "https://api.imgur.com/oauth2/authorize?client_id=abc&response_type=Code&state=test";
            Assert.AreEqual(expected, endpoint.GetAuthorizationUrl(OAuth2ResponseType.Code, "test"));
        }

        [TestMethod]
        public void GetAuthorizationUrl_SetStateNull_AreEqual()
        {
            var imgurClient = new ImgurClient("xyz", "deb");
            var endpoint = new OAuth2Endpoint(imgurClient);
            var expected = "https://api.imgur.com/oauth2/authorize?client_id=xyz&response_type=Code&state=";
            Assert.AreEqual(expected, endpoint.GetAuthorizationUrl(OAuth2ResponseType.Code, null));
        }

        [TestMethod]
        public async Task GetTokenByCodeAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(OAuth2EndpointResponses.OAuth2TokenCodeResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/oauth2/token"), fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var token = await endpoint.GetTokenByCodeAsync("12345");

            Assert.AreEqual("CodeResponse", token.AccessToken);
            Assert.AreEqual("45344", token.AccountId);
            Assert.AreEqual("2132d34234jkljj84ce0c16fjkljfsdfdc70", token.RefreshToken);
            Assert.AreEqual("bearer", token.TokenType);
            Assert.AreEqual(3600, token.ExpiresIn);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public async Task GetTokenByCodeAsync_ThrowsImgurException()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(OAuth2EndpointResponses.OAuth2TokenResponseError)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/oauth2/token"), fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            await endpoint.GetTokenByCodeAsync("12345");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetTokenByCodeAsync_WithNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            await endpoint.GetTokenByCodeAsync(null);
        }

        [TestMethod]
        public async Task GetTokenByPinAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(OAuth2EndpointResponses.OAuth2TokenPinResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/oauth2/token"), fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var token = await endpoint.GetTokenByPinAsync("4839");

            Assert.AreEqual("PinResponse", token.AccessToken);
            Assert.AreEqual("45344", token.AccountId);
            Assert.AreEqual("2132d34234jkljj84ce0c16fjkljfsdfdc70", token.RefreshToken);
            Assert.AreEqual("bearer", token.TokenType);
            Assert.AreEqual(3600, token.ExpiresIn);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetTokenByPinAsync_WithNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            await endpoint.GetTokenByPinAsync(null);
        }

        [TestMethod]
        public async Task GetTokenByRefreshTokenAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(OAuth2EndpointResponses.OAuth2TokenRefreshTokenResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/oauth2/token"), fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var token = await endpoint.GetTokenByRefreshTokenAsync("xhjhjhj");

            Assert.AreEqual("RefreshTokenResponse", token.AccessToken);
            Assert.AreEqual("45344", token.AccountId);
            Assert.AreEqual("2132d34234jkljj84ce0c16fjkljfsdfdc70", token.RefreshToken);
            Assert.AreEqual("bearer", token.TokenType);
            Assert.AreEqual(3600, token.ExpiresIn);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetTokenByRefreshTokenAsync_WithNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            await endpoint.GetTokenByRefreshTokenAsync(null);
        }
    }
}