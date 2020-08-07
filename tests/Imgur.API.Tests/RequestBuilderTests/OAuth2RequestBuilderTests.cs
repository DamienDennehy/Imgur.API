using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.RequestBuilders;
using Xunit;

namespace Imgur.API.Tests.RequestBuilderTests
{
    public class OAuth2RequestBuilderTests
    {
        [Fact]
        public void GetTokenByRefreshTokenRequest_WithClientIdNull_ThrowsArgumentNullException()
        {
            var exception =
                Record.Exception(() =>
                OAuth2RequestBuilder.GetTokenByRefreshTokenRequest("url",
                                                                   "ABChjfhjhjdhfjksdfsdfsdfs",
                                                                   null,
                                                                   "clientSecret"));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("clientId", argNullException.ParamName);
        }

        [Fact]
        public void GetTokenByRefreshTokenRequest_WithClientSecretNull_ThrowsArgumentNullException()
        {
            var exception =
                Record.Exception(() =>
                OAuth2RequestBuilder.GetTokenByRefreshTokenRequest("url",
                                                                   "ABChjfhjhjdhfjksdfsdfsdfs",
                                                                   "clientId",
                                                                   null));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("clientSecret", argNullException.ParamName);
        }

        [Fact]
        public void GetTokenByRefreshTokenRequest_WithEndpointUrlNull_ThrowsArgumentNullException()
        {
            var exception =
                Record.Exception(() => 
                OAuth2RequestBuilder.GetTokenByRefreshTokenRequest(null,
                                                                   "ABChjfhjhjdhfjksdfsdfsdfs",
                                                                   "clientId",
                                                                   "clientSecret"));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("url", argNullException.ParamName);
        }

        [Fact]
        public async Task GetTokenByRefreshTokenRequest_WithRefreshTokenEqual()
        {
            var request = OAuth2RequestBuilder.GetTokenByRefreshTokenRequest("https://api.imgur.com/oauth2/token",
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
            var exception =
                Record.Exception(() => 
                OAuth2RequestBuilder.GetTokenByRefreshTokenRequest("url",
                                                                   null,
                                                                   "clientId",
                                                                   "clientSecret"));
            
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("refreshToken", argNullException.ParamName);
        }
    }
}
