using System;
using Imgur.API.Tests.Mocks;
using Xunit;

// ReSharper disable ExceptionNotDocumented
// ReSharper disable ThrowingSystemException

namespace Imgur.API.Tests.AuthenticationTests
{
    public class ApiClientTests
    {
        [Fact]
        public void ClientId_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new MockApiClient(null, "ClientSecret"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void ClientSecret_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new MockApiClient("ClientId", null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void OAuth2_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new MockApiClient("ClientId", "ClientSecret", null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void OAuth2Token_SetByConstructor_AreSame()
        {
            var oAuth2Token = new MockOAuth2Token().GetOAuth2Token();
            var client = new MockApiClient("ClientId", "ClientSecret", oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetBySetOAuth2Token_AreSame()
        {
            var oAuth2Token = new MockOAuth2Token().GetOAuth2Token();
            var client = new MockApiClient("ClientId", "ClientSecret");

            Assert.Null(client.OAuth2Token);
            client.SetOAuth2Token(oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetBySetOAuth2Token_Null()
        {
            var oAuth2Token = new MockOAuth2Token().GetOAuth2Token();
            var client = new MockApiClient("ClientId", "ClientSecret");

            Assert.Null(client.OAuth2Token);
            client.SetOAuth2Token(oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
            client.SetOAuth2Token(null);
            Assert.Null(client.OAuth2Token);
        }

        [Fact]
        public void RateLimit_SetByConstructor1_NotNull()
        {
            var client = new MockApiClient("ClientId", "ClientSecret");
            Assert.NotNull(client.RateLimit);
        }

        [Fact]
        public void RateLimit_SetByConstructor2_NotNull()
        {
            var oAuth2Token = new MockOAuth2Token().GetOAuth2Token();
            var client = new MockApiClient("ClientId", "ClientSecret", oAuth2Token);
            Assert.NotNull(client.RateLimit);
        }
    }
}