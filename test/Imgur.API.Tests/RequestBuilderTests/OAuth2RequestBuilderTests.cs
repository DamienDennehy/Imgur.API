using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.RequestBuilders;
using Xunit;

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.RequestBuilderTests
{
    public class OAuth2RequestBuilderTests
    {
        [Fact]
        public void GetTokenByCodeRequest_WithClientIdNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var exception =
                Record.Exception(() => requestBuilder.GetTokenByCodeRequest("url", "123code", null, "secret"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetTokenByCodeRequest_WithClientSecretNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var exception =
                Record.Exception(
                    () =>
                        requestBuilder.GetTokenByCodeRequest("https://api.imgur.com/oauth2/token", "123code", "clientId",
                            null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetTokenByCodeRequest_WithCodeEqual()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var request = requestBuilder.GetTokenByCodeRequest("https://api.imgur.com/oauth2/token", "123code", "123",
                "1234");

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/oauth2/token", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.NotNull(request.Content);

            var expected = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal("client_id=123&client_secret=1234&grant_type=authorization_code&code=123code", expected);
        }

        [Fact]
        public void GetTokenByCodeRequest_WithEndpointUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var exception =
                Record.Exception(() => requestBuilder.GetTokenByCodeRequest(null, "123code", "clientId", "clientSecret"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetTokenByCodeRequest_WithTokenNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new OAuth2RequestBuilder();
            var exception =
                Record.Exception(() => requestBuilder.GetTokenByCodeRequest("url", null, "clientId", "clientSecret"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetTokenByPinRequest_WithClientIdNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var exception =
                Record.Exception(() => requestBuilder.GetTokenByPinRequest("url", "123", null, "clientSecret"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetTokenByPinRequest_WithClientSecretNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var exception = Record.Exception(() => requestBuilder.GetTokenByPinRequest("url", "123", "clientId", null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetTokenByPinRequest_WithEndpointUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var exception =
                Record.Exception(() => requestBuilder.GetTokenByPinRequest(null, "123", "clientId", "clientSecret"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetTokenByPinRequest_WithPinEqual()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var request = requestBuilder.GetTokenByPinRequest("https://api.imgur.com/oauth2/token", "4899", "123",
                "1234");

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/oauth2/token", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.NotNull(request.Content);

            var expected = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal("client_id=123&client_secret=1234&grant_type=pin&pin=4899", expected);
        }

        [Fact]
        public void GetTokenByPinRequest_WithPinNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var exception =
                Record.Exception(() => requestBuilder.GetTokenByPinRequest("url", null, "clientId", "clientSecret"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetTokenByRefreshTokenRequest_WithClientIdNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var exception =
                Record.Exception(
                    () =>
                        requestBuilder.GetTokenByRefreshTokenRequest("url", "ABChjfhjhjdhfjksdfsdfsdfs", null,
                            "clientSecret"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetTokenByRefreshTokenRequest_WithClientSecretNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var exception =
                Record.Exception(
                    () =>
                        requestBuilder.GetTokenByRefreshTokenRequest("url", "ABChjfhjhjdhfjksdfsdfsdfs", "clientId",
                            null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetTokenByRefreshTokenRequest_WithEndpointUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var exception =
                Record.Exception(
                    () =>
                        requestBuilder.GetTokenByRefreshTokenRequest(null, "ABChjfhjhjdhfjksdfsdfsdfs", "clientId",
                            "clientSecret"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetTokenByRefreshTokenRequest_WithRefreshTokenEqual()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var request = requestBuilder.GetTokenByRefreshTokenRequest("https://api.imgur.com/oauth2/token",
                "ABChjfhjhjdhfjksdfsdfsdfs", "123", "1234");

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/oauth2/token", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.NotNull(request.Content);

            var expected = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal(
                "client_id=123&client_secret=1234&grant_type=refresh_token&refresh_token=ABChjfhjhjdhfjksdfsdfsdfs",
                expected);
        }

        [Fact]
        public void GetTokenByRefreshTokenRequest_WithTokenNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new OAuth2RequestBuilder();

            var exception =
                Record.Exception(
                    () => requestBuilder.GetTokenByRefreshTokenRequest("url", null, "clientId", "clientSecret"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}