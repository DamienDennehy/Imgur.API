using System;
using Imgur.API.Authentication.Impl;
using Imgur.API.Tests.Mocks;
using Xunit;

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.AuthenticationTests
{
    public class MashapeClientTests
    {
        [Fact]
        public void ClientId_SetByConstructor_Equal()
        {
            var client = new MashapeClient("ClientId", "ClientSecret", "MashapeKey");
            Assert.Equal("ClientId", client.ClientId);
        }

        [Fact]
        public void ClientId_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new MashapeClient(null, "ClientSecret", "MashapeKey"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void ClientSecret_SetByConstructor_Equal()
        {
            var client = new MashapeClient("ClientId", "ClientSecret", "MashapeKey");
            Assert.Equal("ClientSecret", client.ClientSecret);
        }

        [Fact]
        public void ClientSecret_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new MashapeClient("ClientId", null, "MashapeKey"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void EndpointUrl_IsImgurUrl()
        {
            var client = new MashapeClient("ClientId", "ClientSecret", "MashapeKey");
            Assert.Equal("https://imgur-apiv3.p.mashape.com/3/", client.EndpointUrl);
        }

        [Fact]
        public void MashapeKey_SetByConstructor_Equal()
        {
            var client = new MashapeClient("ClientId", "ClientSecret", "MashapeKey");
            Assert.Equal("MashapeKey", client.MashapeKey);
        }

        [Fact]
        public void MashapeKey_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new MashapeClient("ClientId", "ClientSecret", null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void OAuth2Token_SetByConstructor_AreSame()
        {
            var oAuth2Token = new MockOAuth2Token().GetOAuth2Token();
            var client = new MashapeClient("ClientId", "ClientSecret", "MashapeKey", oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new MashapeClient("ClientId", "MashapeKey", null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void RateLimit_SetByConstructor_NotNull()
        {
            var client = new MashapeClient("ClientId", "ClientSecret", "MashapeKey");
            Assert.NotNull(client.RateLimit);
        }
    }
}