using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public class OAuth2EndpointTests : TestBase
    {
        [Fact]
        public void GetAuthorizationUrl_SetState_Equal()
        {
            var client = new ImgurClient("abc", "ioa");
            var endpoint = new OAuth2Endpoint(client);
            var expected = "https://api.imgur.com/oauth2/authorize?client_id=abc&response_type=code&state=test";
            Assert.Equal(expected, endpoint.GetAuthorizationUrl(OAuth2ResponseType.Code, "test"));
        }

        [Fact]
        public void GetAuthorizationUrl_SetStateNull_Equal()
        {
            var client = new ImgurClient("xyz", "deb");
            var endpoint = new OAuth2Endpoint(client);
            var expected = "https://api.imgur.com/oauth2/authorize?client_id=xyz&response_type=code&state=";
            Assert.Equal(expected, endpoint.GetAuthorizationUrl(OAuth2ResponseType.Code));
        }

        [Fact]
        public async Task GetTokenByCodeAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/oauth2/token";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockOAuth2EndpointResponses.GetTokenByCode)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var token = await endpoint.GetTokenByCodeAsync("12345").ConfigureAwait(false);

            Assert.Equal("CodeResponse", token.AccessToken);
            Assert.Equal("2132d34234jkljj84ce0c16fjkljfsdfdc70", token.RefreshToken);
            Assert.Equal("bearer", token.TokenType);
            Assert.Equal(2419200, token.ExpiresIn);
            Assert.Equal("Bob", token.AccountUsername);
            Assert.Equal("45344", token.AccountId);
        }

        [Fact]
        public async Task GetTokenByCodeAsync_ThrowsImgurException()
        {
            var mockUrl = "https://api.imgur.com/oauth2/token";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(MockOAuth2EndpointResponses.OAuth2TokenResponseError)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetTokenByCodeAsync("12345").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public async Task GetTokenByCodeAsync_WithClientSecretNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123");
            var endpoint = new OAuth2Endpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetTokenByCodeAsync("1234").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "ClientSecret");
        }

        [Fact]
        public async Task GetTokenByCodeAsync_WithCodeNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetTokenByCodeAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "code");
        }

        [Fact]
        public async Task GetTokenByPinAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/oauth2/token";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockOAuth2EndpointResponses.GetTokenByPin)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var token = await endpoint.GetTokenByPinAsync("4839").ConfigureAwait(false);

            Assert.Equal("PinResponse", token.AccessToken);
            Assert.Equal("2132d34234jkljj84ce0c16fjkljfsdfdc70", token.RefreshToken);
            Assert.Equal("bearer", token.TokenType);
            Assert.Equal(2419200, token.ExpiresIn);
            Assert.Equal("Bob", token.AccountUsername);
            Assert.Equal("45344", token.AccountId);
        }

        [Fact]
        public async Task GetTokenByPinAsync_WithClientSecretNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123");
            var endpoint = new OAuth2Endpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetTokenByPinAsync("1234").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "ClientSecret");
        }

        [Fact]
        public async Task GetTokenByPinAsync_WithPinNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetTokenByPinAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetTokenByRefreshTokenAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/oauth2/token";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockOAuth2EndpointResponses.GetTokenByRefreshToken)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var token = await endpoint.GetTokenByRefreshTokenAsync("xhjhjhj").ConfigureAwait(false);

            Assert.Equal("RefreshTokenResponse", token.AccessToken);
            Assert.Equal("2132d34234jkljj84ce0c16fjkljfsdfdc70", token.RefreshToken);
            Assert.Equal("bearer", token.TokenType);
            Assert.Equal(2419200, token.ExpiresIn);
            Assert.Equal("Bob", token.AccountUsername);
            Assert.Equal("45344", token.AccountId);
        }

        [Fact]
        public async Task GetTokenByRefreshTokenAsync_WithClientSecretNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123");
            var endpoint = new OAuth2Endpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetTokenByRefreshTokenAsync("ahkjhkjhc").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "ClientSecret");
        }

        [Fact]
        public async Task GetTokenByRefreshTokenAsync_WithTokenNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new OAuth2Endpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetTokenByRefreshTokenAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}