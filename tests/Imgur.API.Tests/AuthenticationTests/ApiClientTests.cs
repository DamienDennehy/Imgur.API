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
            var client = new ApiClient("ClientId123", "ClientSecret123");

            Assert.Equal("ClientId123", client.ClientId);
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
            var client = new ApiClient("ClientId123", "ClientSecret123");

            Assert.Equal("ClientSecret123", client.ClientSecret);
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
        public void SetOAuth2Token_WithNullAccessToken_ThrowsArgumentNullException()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString(),
                AccountId = Guid.NewGuid().ToString(),
                AccountUsername = Guid.NewGuid().ToString(),
                ExpiresIn = DateTime.Now.Minute,
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
        public void SetOAuth2Token_WithNullAccessId_ThrowsArgumentNullException()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString(),
                AccountId = null,
                AccountUsername = Guid.NewGuid().ToString(),
                ExpiresIn = DateTime.Now.Minute,
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
        public void SetOAuth2Token_WithNullAccountUsername_ThrowsArgumentNullException()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString(),
                AccountId = Guid.NewGuid().ToString(),
                AccountUsername = null,
                ExpiresIn = DateTime.Now.Minute,
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
        public void OAuth2Token_SetByClientIdConstructor_AreSame()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString(),
                AccountId = Guid.NewGuid().ToString(),
                AccountUsername = Guid.NewGuid().ToString(),
                ExpiresIn = DateTime.Now.Minute,
                RefreshToken = Guid.NewGuid().ToString(),
                TokenType = Guid.NewGuid().ToString()
            };

            var client = new ApiClient("ClientId");
            client.SetOAuth2Token(oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetBySetOAuth2Token_AreSame()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString(),
                AccountId = Guid.NewGuid().ToString(),
                AccountUsername = Guid.NewGuid().ToString(),
                ExpiresIn = DateTime.Now.Minute,
                RefreshToken = Guid.NewGuid().ToString(),
                TokenType = Guid.NewGuid().ToString()
            };

            var client = new ApiClient("ClientId", "ClientSecret");

            Assert.Null(client.OAuth2Token);
            client.SetOAuth2Token(oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetBySetOAuth2Token_Null()
        {
            var oAuth2Token = new OAuth2Token
            {
                AccessToken = Guid.NewGuid().ToString(),
                AccountId = Guid.NewGuid().ToString(),
                AccountUsername = Guid.NewGuid().ToString(),
                ExpiresIn = DateTime.Now.Minute,
                RefreshToken = Guid.NewGuid().ToString(),
                TokenType = Guid.NewGuid().ToString()
            };

            var client = new ApiClient("ClientId", "ClientSecret");

            Assert.Null(client.OAuth2Token);
            client.SetOAuth2Token(oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
            client.SetOAuth2Token(null);
            Assert.Null(client.OAuth2Token);
        }
    }
}
