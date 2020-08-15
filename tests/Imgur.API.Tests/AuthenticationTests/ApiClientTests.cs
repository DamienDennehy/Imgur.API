using System;
using Imgur.API.Authentication;
using Imgur.API.Models;
using Xunit;

namespace Imgur.API.Tests.AuthenticationTests
{
    public class ApiClientTests
    {
        [Fact]
        public void ClientId_SetByConstructor_AreEqual()
        {
            var apiClient = new ApiClient("ClientId123", "ClientSecret123");

            Assert.Equal("ClientId123", apiClient.ClientId);
        }

        [Fact]
        public void BaseAddress_SetByConstructor_AreEqual()
        {
            var apiClient = new ApiClient("ClientId123", "ClientSecret123");

            Assert.Equal("https://api.imgur.com/3/", apiClient.BaseAddress);
        }

        [Fact]
        public void ClientId_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new ApiClient(null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("clientId", argNullException.ParamName);
        }

        [Fact]
        public void ClientSecret_SetByConstructor_AreEqual()
        {
            var apiClient = new ApiClient("ClientId123", "ClientSecret123");

            Assert.Equal("ClientSecret123", apiClient.ClientSecret);
        }

        [Fact]
        public void ClientSecret_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new ApiClient("ClientId", clientSecret: null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("clientSecret", argNullException.ParamName);
        }

        [Fact]
        public void SetOAuth2Token_WithNullAccessToken_ThrowsArgumentException()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = null,
                AccountId = (int)DateTime.Now.Ticks,
                AccountUsername = Guid.NewGuid().ToString(),
                ExpiresIn = (int)DateTime.Now.Ticks,
                RefreshToken = Guid.NewGuid().ToString(),
                TokenType = Guid.NewGuid().ToString()
            };

            var exception = Record.Exception(() =>
            {
                var apiClient = new ApiClient("ClientId");
                apiClient.SetOAuth2Token(oAuth2Token);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void SetOAuth2Token_WithNullAccountId_ThrowsArgumentException()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString(),
                AccountId = 0,
                AccountUsername = Guid.NewGuid().ToString(),
                ExpiresIn = (int)DateTime.Now.Ticks,
                RefreshToken = Guid.NewGuid().ToString(),
                TokenType = Guid.NewGuid().ToString()
            };

            var exception = Record.Exception(() =>
            {
                var apiClient = new ApiClient("ClientId");
                apiClient.SetOAuth2Token(oAuth2Token);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void SetOAuth2Token_WithNullAccountUsername_ThrowsArgumentException()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString(),
                AccountId = (int)DateTime.Now.Ticks,
                AccountUsername = null,
                ExpiresIn = (int)DateTime.Now.Ticks,
                RefreshToken = Guid.NewGuid().ToString(),
                TokenType = Guid.NewGuid().ToString()
            };

            var exception = Record.Exception(() =>
            {
                var apiClient = new ApiClient("ClientId");
                apiClient.SetOAuth2Token(oAuth2Token);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void SetOAuth2Token_WithEmptyExpiresIn_ThrowsArgumentException()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString(),
                AccountId = (int)DateTime.Now.Ticks,
                AccountUsername = Guid.NewGuid().ToString(),
                ExpiresIn = 0,
                RefreshToken = Guid.NewGuid().ToString(),
                TokenType = Guid.NewGuid().ToString()
            };

            var exception = Record.Exception(() =>
            {
                var apiClient = new ApiClient("ClientId");
                apiClient.SetOAuth2Token(oAuth2Token);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void SetOAuth2Token_WithNullRefreshToken_ThrowsArgumentException()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString(),
                AccountId = (int)DateTime.Now.Ticks,
                AccountUsername = Guid.NewGuid().ToString(),
                ExpiresIn = (int)DateTime.Now.Ticks,
                RefreshToken = null,
                TokenType = Guid.NewGuid().ToString()
            };

            var exception = Record.Exception(() =>
            {
                var apiClient = new ApiClient("ClientId");
                apiClient.SetOAuth2Token(oAuth2Token);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void SetOAuth2Token_WithNullTokenType_ThrowsArgumentException()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString(),
                AccountId = (int)DateTime.Now.Ticks,
                AccountUsername = Guid.NewGuid().ToString(),
                ExpiresIn = (int)DateTime.Now.Ticks,
                RefreshToken = Guid.NewGuid().ToString(),
                TokenType = null
            };

            var exception = Record.Exception(() =>
            {
                var apiClient = new ApiClient("ClientId");
                apiClient.SetOAuth2Token(oAuth2Token);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void OAuth2Token_SetByClientIdConstructor_AreSame()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString(),
                AccountId = (int)DateTime.Now.Ticks,
                AccountUsername = Guid.NewGuid().ToString(),
                ExpiresIn = (int)DateTime.Now.Ticks,
                RefreshToken = Guid.NewGuid().ToString(),
                TokenType = Guid.NewGuid().ToString()
            };

            var apiClient = new ApiClient("ClientId");
            apiClient.SetOAuth2Token(oAuth2Token);
            Assert.Same(oAuth2Token, apiClient.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetBySetOAuth2Token_AreSame()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString(),
                AccountId = (int)DateTime.Now.Ticks,
                AccountUsername = Guid.NewGuid().ToString(),
                ExpiresIn = (int)DateTime.Now.Ticks,
                RefreshToken = Guid.NewGuid().ToString(),
                TokenType = Guid.NewGuid().ToString()
            };

            var apiClient = new ApiClient("ClientId", "ClientSecret");

            Assert.Null(apiClient.OAuth2Token);
            apiClient.SetOAuth2Token(oAuth2Token);
            Assert.Same(oAuth2Token, apiClient.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetBySetOAuth2Token_Null()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString(),
                AccountId = (int)DateTime.Now.Ticks,
                AccountUsername = Guid.NewGuid().ToString(),
                ExpiresIn = (int)DateTime.Now.Ticks,
                RefreshToken = Guid.NewGuid().ToString(),
                TokenType = Guid.NewGuid().ToString()
            };

            var apiClient = new ApiClient("ClientId", "ClientSecret");

            Assert.Null(apiClient.OAuth2Token);
            apiClient.SetOAuth2Token(oAuth2Token);
            Assert.Same(oAuth2Token, apiClient.OAuth2Token);
            apiClient.SetOAuth2Token(null);
            Assert.Null(apiClient.OAuth2Token);
        }
    }
}
