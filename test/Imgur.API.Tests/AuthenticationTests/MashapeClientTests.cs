using System;
using Imgur.API.Authentication.Impl;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.AuthenticationTests
{
    public class MashapeClientTests
    {
        [Fact]
        public void ClientId_SetByConstructor_AreEqual()
        {
            var client = new MashapeClient("ClientId123", "MashapeKey");

            Assert.Equal("ClientId123", client.ClientId);
        }

        [Fact]
        public void ClientId_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new MashapeClient(null, "mashapeKey"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "clientId");
        }

        [Fact]
        public void ClientSecret_SetByConstructor_AreEqual()
        {
            var client = new MashapeClient("ClientId123", "ClientSecret123", "MashapeKey");

            Assert.Equal("ClientSecret123", client.ClientSecret);
        }

        [Fact]
        public void ClientSecret_SetNullByConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new MashapeClient("ClientId", null, "mashapeKey"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "clientSecret");
        }

        [Fact]
        public void EndpointUrl_IsMashaeUrl()
        {
            var client = new MashapeClient("ClientId", "MashapeKey");
            Assert.Equal("https://imgur-apiv3.p.mashape.com/3/", client.EndpointUrl);
        }

        [Fact]
        public void MashapeKey_SetNullByConstructor_ThrowArgumentNullException()
        {
            var oAuth2Token = MockOAuth2Token.GetOAuth2Token();

            var exception = Record.Exception(() => new MashapeClient("ClientId", null));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "mashapeKey");

            exception = Record.Exception(() => new MashapeClient("ClientId", "ClientSecret", mashapeKey: null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "mashapeKey");

            exception = Record.Exception(() => new MashapeClient("ClientId", null, oAuth2Token));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "mashapeKey");

            exception = Record.Exception(() => new MashapeClient("ClientId", "ClientSecret", null, oAuth2Token));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "mashapeKey");
        }

        [Fact]
        public void OAuth2Token_SetByClientIdAndSecretConstructor_AreSame()
        {
            var oAuth2Token = MockOAuth2Token.GetOAuth2Token();
            var client = new MashapeClient("ClientId", "ClientSecret", "MashapeKey", oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetByClientIdConstructor_AreSame()
        {
            var oAuth2Token = MockOAuth2Token.GetOAuth2Token();
            var client = new MashapeClient("ClientId", "MashapeKey", oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetBySetOAuth2Token_AreSame()
        {
            var oAuth2Token = MockOAuth2Token.GetOAuth2Token();
            var client = new MashapeClient("ClientId", "MashapeKey");

            Assert.Null(client.OAuth2Token);
            client.SetOAuth2Token(oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetBySetOAuth2Token_Null()
        {
            var oAuth2Token = MockOAuth2Token.GetOAuth2Token();
            var client = new MashapeClient("ClientId", "MashapeKey");

            Assert.Null(client.OAuth2Token);
            client.SetOAuth2Token(oAuth2Token);
            Assert.Same(oAuth2Token, client.OAuth2Token);
            client.SetOAuth2Token(null);
            Assert.Null(client.OAuth2Token);
        }

        [Fact]
        public void OAuth2Token_SetNullByClientIdAndSecretConstructor_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new MashapeClient("ClientId", "ClientSecret", "MashapeKey", null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "oAuth2Token");
        }

        [Fact]
        public void OAuth2Token_SetNullByClientIdConstructor_ThrowArgumentNullException()
        {
            var exception =
                Record.Exception(() => new MashapeClient("ClientId", oAuth2Token: null, mashapeKey: "MashapeKey"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "oAuth2Token");
        }

        [Fact]
        public void RateLimit_AutoProperty_NotNull()
        {
            var client = new MashapeClient("ClientId", "MashapeKey");
            Assert.NotNull(client.RateLimit);
        }
    }
}