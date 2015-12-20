using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.RequestBuilders;
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
            var requestBuilder = new OAuth2RequestBuilder();
            requestBuilder.GetTokenByCodeRequest("url", "123code", null, "secret");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByCodeRequest_WithClientSecretNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new OAuth2RequestBuilder();
            requestBuilder.GetTokenByCodeRequest("https://api.imgur.com/oauth2/token", "123code", "clientId", null);
        }

        [TestMethod]
        public async Task GetTokenByCodeRequest_WithCodeAreEqual()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var request = requestBuilder.GetTokenByCodeRequest("https://api.imgur.com/oauth2/token", "123code", "123",
                "1234");

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
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new OAuth2RequestBuilder();
            requestBuilder.GetTokenByCodeRequest(null, "123code", "clientId", "clientSecret");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByCodeRequest_WithTokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new OAuth2RequestBuilder();
            requestBuilder.GetTokenByCodeRequest("url", null, "clientId", "clientSecret");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByPinRequest_WithClientIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new OAuth2RequestBuilder();
            requestBuilder.GetTokenByPinRequest("url", "123", null, "clientSecret");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByPinRequest_WithClientSecretNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new OAuth2RequestBuilder();
            requestBuilder.GetTokenByPinRequest("url", "123", "clientId", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByPinRequest_WithEndpointUrlNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new OAuth2RequestBuilder();
            requestBuilder.GetTokenByPinRequest(null, "123", "clientId", "clientSecret");
        }

        [TestMethod]
        public async Task GetTokenByPinRequest_WithPinAreEqual()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var request = requestBuilder.GetTokenByPinRequest("https://api.imgur.com/oauth2/token", "4899", "123",
                "1234");

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
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new OAuth2RequestBuilder();
            requestBuilder.GetTokenByPinRequest("url", null, "clientId", "clientSecret");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByRefreshTokenRequest_WithClientIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new OAuth2RequestBuilder();
            requestBuilder.GetTokenByRefreshTokenRequest("url", "ABChjfhjhjdhfjksdfsdfsdfs", null, "clientSecret");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByRefreshTokenRequest_WithClientSecretNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new OAuth2RequestBuilder();
            requestBuilder.GetTokenByRefreshTokenRequest("url", "ABChjfhjhjdhfjksdfsdfsdfs", "clientId", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetTokenByRefreshTokenRequest_WithEndpointUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new OAuth2RequestBuilder();
            requestBuilder.GetTokenByRefreshTokenRequest(null, "ABChjfhjhjdhfjksdfsdfsdfs", "clientId", "clientSecret");
        }

        [TestMethod]
        public async Task GetTokenByRefreshTokenRequest_WithRefreshTokenAreEqual()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var request = requestBuilder.GetTokenByRefreshTokenRequest("https://api.imgur.com/oauth2/token",
                "ABChjfhjhjdhfjksdfsdfsdfs", "123", "1234");

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
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new OAuth2RequestBuilder();
            requestBuilder.GetTokenByRefreshTokenRequest("url", null, "clientId", "clientSecret");
        }
    }
}