using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Tests.FakeResponses;
using Imgur.API.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class OAuth2EndpointTests : TestBase
    {
        [TestMethod]
        public void GetAuthorizationUrl_SetState_AreEqual()
        {
            var client = new ImgurClient("abc", "ioa");
            var endpoint = new OAuth2Endpoint(client);
            var expected = "https://api.imgur.com/oauth2/authorize?client_id=abc&response_type=Code&state=test";
            Assert.AreEqual(expected, endpoint.GetAuthorizationUrl(OAuth2ResponseType.Code, "test"));
        }

        [TestMethod]
        public void GetAuthorizationUrl_SetStateNull_AreEqual()
        {
            var client = new ImgurClient("xyz", "deb");
            var endpoint = new OAuth2Endpoint(client);
            var expected = "https://api.imgur.com/oauth2/authorize?client_id=xyz&response_type=Code&state=";
            Assert.AreEqual(expected, endpoint.GetAuthorizationUrl(OAuth2ResponseType.Code));
        }

        [TestMethod]
        public async Task GetTokenByCodeAsync_AreEqual()
        {
            var fakeUrl = "https://api.imgur.com/oauth2/token";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(OAuth2EndpointResponses.GetTokenByCodeAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var token = await endpoint.GetTokenByCodeAsync("12345").ConfigureAwait(false);

            Assert.AreEqual("CodeResponse", token.AccessToken);
            Assert.AreEqual("2132d34234jkljj84ce0c16fjkljfsdfdc70", token.RefreshToken);
            Assert.AreEqual("bearer", token.TokenType);
            Assert.AreEqual(2419200, token.ExpiresIn);
            Assert.AreEqual("Bob", token.AccountUsername);
            Assert.AreEqual("45344", token.AccountId);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public async Task GetTokenByCodeAsync_ThrowsImgurException()
        {
            var fakeUrl = "https://api.imgur.com/oauth2/token";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(OAuth2EndpointResponses.OAuth2TokenResponseError)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            await endpoint.GetTokenByCodeAsync("12345").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetTokenByCodeAsync_WithCodeNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(client);
            await endpoint.GetTokenByCodeAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetTokenByPinAsync_AreEqual()
        {
            var fakeUrl = "https://api.imgur.com/oauth2/token";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(OAuth2EndpointResponses.GetTokenByPinAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var token = await endpoint.GetTokenByPinAsync("4839").ConfigureAwait(false);

            Assert.AreEqual("PinResponse", token.AccessToken);
            Assert.AreEqual("2132d34234jkljj84ce0c16fjkljfsdfdc70", token.RefreshToken);
            Assert.AreEqual("bearer", token.TokenType);
            Assert.AreEqual(2419200, token.ExpiresIn);
            Assert.AreEqual("Bob", token.AccountUsername);
            Assert.AreEqual("45344", token.AccountId);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetTokenByPinAsync_WithPinNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(client);
            await endpoint.GetTokenByPinAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetTokenByRefreshTokenAsync_AreEqual()
        {
            var fakeUrl = "https://api.imgur.com/oauth2/token";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(OAuth2EndpointResponses.GetTokenByRefreshTokenAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var token = await endpoint.GetTokenByRefreshTokenAsync("xhjhjhj").ConfigureAwait(false);

            Assert.AreEqual("RefreshTokenResponse", token.AccessToken);
            Assert.AreEqual("2132d34234jkljj84ce0c16fjkljfsdfdc70", token.RefreshToken);
            Assert.AreEqual("bearer", token.TokenType);
            Assert.AreEqual(2419200, token.ExpiresIn);
            Assert.AreEqual("Bob", token.AccountUsername);
            Assert.AreEqual("45344", token.AccountId);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetTokenByRefreshTokenAsync_WithTokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(client);
            await endpoint.GetTokenByRefreshTokenAsync(null).ConfigureAwait(false);
        }
    }
}