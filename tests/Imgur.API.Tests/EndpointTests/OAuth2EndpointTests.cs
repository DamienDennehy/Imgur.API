using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public class OAuth2EndpointTests
    {
        [Fact]
        public void GetAuthorizationUrl_SetState_Equal()
        {
            var apiClient = new ApiClient("abc", "ioa");
            var endpoint = new OAuth2Endpoint(apiClient, new HttpClient());
            var expected = "https://api.imgur.com/oauth2/authorize?client_id=abc&response_type=token&state=test";
            Assert.Equal(expected, endpoint.GetAuthorizationUrl("test"));
        }

        [Fact]
        public void GetAuthorizationUrl_SetStateNull_Equal()
        {
            var apiClient = new ApiClient("xyz", "deb");
            var endpoint = new OAuth2Endpoint(apiClient, new HttpClient());
            var expected = "https://api.imgur.com/oauth2/authorize?client_id=xyz&response_type=token&state=";
            Assert.Equal(expected, endpoint.GetAuthorizationUrl());
        }

        [Fact]
        public async Task GetTokenAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/oauth2/token";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockOAuth2Responses.GetTokenResponse)
            };

            var apiClient = new ApiClient("123", "445");
            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new OAuth2Endpoint(apiClient, httpClient);
            var token = await endpoint.GetTokenAsync("xhjhjhj");

            Assert.Equal("6e079993b20f45fab3c22ed489a6f454", token.AccessToken);
            Assert.Equal("e325da142cd545298ef68868824d3b8a", token.RefreshToken);
            Assert.Equal("bearer", token.TokenType);
            Assert.Equal(315360000, token.ExpiresIn);
            Assert.Equal("A8XTgSW8pWrNCFwR", token.AccountUsername);
            Assert.Equal(135798223, token.AccountId);
        }

        [Fact]
        public async Task GetTokenAsync_WithClientSecretNull_ThrowsInvalidOperationException()
        {
            var apiClient = new ApiClient("123");
            var endpoint = new OAuth2Endpoint(apiClient, new HttpClient());

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.GetTokenAsync("1234");
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (InvalidOperationException)exception;
            Assert.Equal("refreshToken", argNullException.ParamName);
        }

        [Fact]
        public async Task GetTokenAsync_WithTokenNull_ThrowsArgumentNullException()
        {
            var apiClient = new ApiClient("123", "1234");
            var endpoint = new OAuth2Endpoint(apiClient, new HttpClient());

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.GetTokenAsync(null);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("refreshToken", argNullException.ParamName);
        }
    }
}
