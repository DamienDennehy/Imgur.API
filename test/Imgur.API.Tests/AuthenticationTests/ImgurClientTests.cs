using System;
using Imgur.API.Authentication.Impl;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.AuthenticationTests
{
    public class ImgurClientTests
    {
        [Fact]
        public void ClientId_SetByConstructor_AreEqual()
        {
            var client = new ImgurClient("ClientId123", "ClientSecret123");

            Assert.Equal("ClientId123", client.ClientId);
        }

        [Fact]
        public void ClientId_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new ImgurClient(null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "clientId");
        }

        [Fact]
        public void ClientSecret_SetByConstructor_AreEqual()
        {
            var client = new ImgurClient("ClientId123", "ClientSecret123");

            Assert.Equal("ClientSecret123", client.ClientSecret);
        }

        [Fact]
        public void ClientSecret_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new ImgurClient("ClientId", clientSecret: null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "clientSecret");
        }

        [Fact]
        public void EndpointUrl_IsImgurUrl()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            Assert.Equal("https://api.imgur.com/3/", client.EndpointUrl);
        }

        [Fact]
        public void OAuth2Token_SetByClientIdAndSecretConstructor_AreSame()
        {
            var oAuth2Token = MockOAuth2Token.GetOAuth2Token();
            var client = new ImgurClient("ClientId", "ClientSecret", oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetByClientIdConstructor_AreSame()
        {
            var oAuth2Token = MockOAuth2Token.GetOAuth2Token();
            var client = new ImgurClient("ClientId", oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetBySetOAuth2Token_AreSame()
        {
            var oAuth2Token = MockOAuth2Token.GetOAuth2Token();
            var client = new ImgurClient("ClientId", "ClientSecret");

            Assert.Null(client.OAuth2Token);
            client.SetOAuth2Token(oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetBySetOAuth2Token_Null()
        {
            var oAuth2Token = MockOAuth2Token.GetOAuth2Token();
            var client = new ImgurClient("ClientId", "ClientSecret");

            Assert.Null(client.OAuth2Token);
            client.SetOAuth2Token(oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
            client.SetOAuth2Token(null);
            Assert.Null(client.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetNullByClientIdAndSecretConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new ImgurClient("ClientId", "ClientSecret", null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "oAuth2Token");
        }

        [Fact]
        public void OAuth2Token_SetNullByClientIdConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new ImgurClient("ClientId", oAuth2Token: null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "oAuth2Token");
        }

        [Fact]
        public void RateLimit_AutoProperty_NotNull()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            Assert.NotNull(client.RateLimit);
        }
    }
}