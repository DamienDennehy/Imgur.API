using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.RequestBuilders
{
    [TestClass]
    public class OAuth2RequestBuilderTests
    {
        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByCodeRequest_WithClientIdNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            endpoint.RequestBuilder.GetTokenByCodeRequest(endpoint.TokenEndpointUrl, "123code", null,
                endpoint.ApiClient.ClientSecret);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByCodeRequest_WithClientSecretNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            endpoint.RequestBuilder.GetTokenByCodeRequest(endpoint.TokenEndpointUrl, "123code",
                endpoint.ApiClient.ClientId, null);
        }

        [TestMethod]
        public async Task GetTokenByCodeRequest_WithCodeAreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);

            var request = endpoint.RequestBuilder.GetTokenByCodeRequest(endpoint.TokenEndpointUrl, "123code",
                endpoint.ApiClient.ClientId, endpoint.ApiClient.ClientSecret);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/oauth2/token", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.IsNotNull(request.Content);

            var expected = await request.Content.ReadAsStringAsync();

            Assert.AreEqual("client_id=123&client_secret=1234&grant_type=authorization_code&code=123code", expected);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByCodeRequest_WithEndpointUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            endpoint.RequestBuilder.GetTokenByCodeRequest(null, "123code", endpoint.ApiClient.ClientId,
                endpoint.ApiClient.ClientSecret);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByCodeRequest_WithTokenNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            endpoint.RequestBuilder.GetTokenByCodeRequest(endpoint.TokenEndpointUrl, null, endpoint.ApiClient.ClientId,
                endpoint.ApiClient.ClientSecret);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByPinRequest_WithClientIdNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            endpoint.RequestBuilder.GetTokenByPinRequest(endpoint.TokenEndpointUrl, "123", null,
                endpoint.ApiClient.ClientSecret);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByPinRequest_WithClientSecretNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            endpoint.RequestBuilder.GetTokenByPinRequest(endpoint.TokenEndpointUrl, "123", endpoint.ApiClient.ClientId,
                null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByPinRequest_WithEndpointUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            endpoint.RequestBuilder.GetTokenByPinRequest(null, "123", endpoint.ApiClient.ClientId,
                endpoint.ApiClient.ClientSecret);
        }

        [TestMethod]
        public async Task GetTokenByPinRequest_WithPinAreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);

            var request = endpoint.RequestBuilder.GetTokenByPinRequest(endpoint.TokenEndpointUrl, "4899",
                endpoint.ApiClient.ClientId, endpoint.ApiClient.ClientSecret);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/oauth2/token", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.IsNotNull(request.Content);

            var expected = await request.Content.ReadAsStringAsync();

            Assert.AreEqual("client_id=123&client_secret=1234&grant_type=pin&pin=4899", expected);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByPinRequest_WithPinNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            endpoint.RequestBuilder.GetTokenByPinRequest(endpoint.TokenEndpointUrl, null, endpoint.ApiClient.ClientId,
                endpoint.ApiClient.ClientSecret);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByRefreshTokenRequest_WithClientIdNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            endpoint.RequestBuilder.GetTokenByRefreshTokenRequest(endpoint.TokenEndpointUrl, "ABChjfhjhjdhfjksdfsdfsdfs",
                null, endpoint.ApiClient.ClientSecret);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByRefreshTokenRequest_WithClientSecretNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            endpoint.RequestBuilder.GetTokenByRefreshTokenRequest(endpoint.TokenEndpointUrl, "ABChjfhjhjdhfjksdfsdfsdfs",
                endpoint.ApiClient.ClientId, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByRefreshTokenRequest_WithEndpointUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            endpoint.RequestBuilder.GetTokenByRefreshTokenRequest(null, "ABChjfhjhjdhfjksdfsdfsdfs",
                endpoint.ApiClient.ClientId, endpoint.ApiClient.ClientSecret);
        }

        [TestMethod]
        public async Task GetTokenByRefreshTokenRequest_WithRefreshTokenAreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);

            var request = endpoint.RequestBuilder.GetTokenByRefreshTokenRequest(endpoint.TokenEndpointUrl,
                "ABChjfhjhjdhfjksdfsdfsdfs", endpoint.ApiClient.ClientId, endpoint.ApiClient.ClientSecret);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/oauth2/token", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.IsNotNull(request.Content);

            var expected = await request.Content.ReadAsStringAsync();

            Assert.AreEqual(
                "client_id=123&client_secret=1234&grant_type=refresh_token&refresh_token=ABChjfhjhjdhfjksdfsdfsdfs",
                expected);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByRefreshTokenRequest_WithTokenNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(imgurClient);
            endpoint.RequestBuilder.GetTokenByRefreshTokenRequest(endpoint.TokenEndpointUrl, null,
                endpoint.ApiClient.ClientId, endpoint.ApiClient.ClientSecret);
        }
    }
}