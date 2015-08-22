using System;
using Imgur.API.Authentication;
using Imgur.API.Authentication.Impl;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Authentication
{
    [TestClass]
    public class OAuth2AuthenticationTests
    {
        [TestMethod]
        public void OAuth2ResponseType_Set_AreEqual()
        {
            var oAuth = Substitute.For<IOAuth2Authentication>();
            oAuth.OAuth2ResponseType.Returns(OAuth2ResponseType.Code);
            Assert.AreEqual(oAuth.OAuth2ResponseType, OAuth2ResponseType.Code);
        }

        [TestMethod]
        public void IOAuth2Token_SetOAuth2Token_ReceivedIsTrue()
        {
            var oAuth = Substitute.For<IOAuth2Authentication>();
            var oAuth2Token = Substitute.For<IOAuth2Token>();
            oAuth.SetOAuth2Token(oAuth2Token);
            oAuth.Received().SetOAuth2Token(oAuth2Token);
        }

        [TestMethod]
        public void OAuth2ResponseType_SetByConstructor_AreEqual()
        {
            var authentication = new OAuth2Authentication(OAuth2ResponseType.Code);
            Assert.AreEqual(OAuth2ResponseType.Code, authentication.OAuth2ResponseType);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void OAuth2Token_SetNull_ThrowArgumentNullException()
        {
            var authentication = new OAuth2Authentication(OAuth2ResponseType.Code);
            authentication.SetOAuth2Token(null);
        }

        [TestMethod]
        public void OAuth2Token_SetToken_IsNotNull()
        {
            var authentication = new OAuth2Authentication(OAuth2ResponseType.Code);
            var token = new OAuth2Token("access_token", "refresh_token", "token_type", "accountId", 3600);
            authentication.SetOAuth2Token(token);
            Assert.IsNotNull(authentication.OAuth2Token);
        }

        [TestMethod]
        public void OAuth2Token_SetToken_AreEqual()
        {
            var authentication = new OAuth2Authentication(OAuth2ResponseType.Code);
            var token = new OAuth2Token("access_token", "refresh_token", "token_type", "accountId", 3600);
            authentication.SetOAuth2Token(token);
            Assert.AreEqual(authentication.OAuth2Token.AccessToken, token.AccessToken);
            Assert.AreEqual(authentication.OAuth2Token.RefreshToken, token.RefreshToken);
            Assert.AreEqual(authentication.OAuth2Token.TokenType, token.TokenType);
            Assert.AreEqual(authentication.OAuth2Token.ExpiresIn, token.ExpiresIn);
            Assert.AreEqual(authentication.OAuth2Token.AccountId, token.AccountId);
        }
    }
}