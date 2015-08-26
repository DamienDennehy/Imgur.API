using System;
using Imgur.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Authentication
{
    [TestClass]
    public class OAuth2TokenTests
    {
        [TestMethod]
        public void IOAuth2Token_Set_AreEqual()
        {
            var oAuth2Token = Substitute.For<IOAuth2Token>();
            oAuth2Token.AccessToken.Returns("access_Token");
            oAuth2Token.AccountId.Returns("account_Id");
            oAuth2Token.ExpiresAt.Returns(DateTimeOffset.MinValue);
            oAuth2Token.ExpiresIn.Returns(1000);
            oAuth2Token.RefreshToken.Returns("refresh_Token");
            oAuth2Token.TokenType.Returns("token_Type");
            Assert.AreEqual("access_Token", oAuth2Token.AccessToken);
            Assert.AreEqual("account_Id", oAuth2Token.AccountId);
            Assert.AreEqual(DateTimeOffset.MinValue, oAuth2Token.ExpiresAt);
            Assert.AreEqual(1000, oAuth2Token.ExpiresIn);
            Assert.AreEqual("refresh_Token", oAuth2Token.RefreshToken);
            Assert.AreEqual("token_Type", oAuth2Token.TokenType);
        }
    }
}