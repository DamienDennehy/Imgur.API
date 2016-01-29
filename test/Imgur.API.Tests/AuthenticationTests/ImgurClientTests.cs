using System;
using Imgur.API.Authentication.Impl;
using Imgur.API.Tests.Mocks;
using Xunit;

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.AuthenticationTests
{
    public class ImgurClientTests
    {
        [Fact]
        public void ClientId_SetByConstructor_Equal()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            Assert.Equal("ClientId", client.ClientId);
        }

        [Fact]
        public void ClientId_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new ImgurClient(null, "ClientSecret"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void ClientSecret_SetByConstructor_Equal()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            Assert.Equal("ClientSecret", client.ClientSecret);
        }

        [Fact]
        public void ClientSecret_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new ImgurClient("ClientId", null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void EndpointUrl_IsImgurUrl()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            Assert.Equal("https://api.imgur.com/3/", client.EndpointUrl);
        }

        [Fact]
        public void OAuth2Token_SetByConstructor_AreSame()
        {
            var oAuth2Token = new MockOAuth2Token().GetOAuth2Token();
            var client = new ImgurClient("ClientId", "ClientSecret", oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new ImgurClient("ClientId", "ClientSecret", null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void RateLimit_SetByInitialization_NotNull()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            Assert.NotNull(client.RateLimit);
        }
    }
}