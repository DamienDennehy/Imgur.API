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
            var token = Substitute.For<IOAuth2Token>();
            token.AccessToken.Returns("access_Token");
            token.AccountId.Returns("account_Id");
            token.ExpiresAt.Returns(DateTimeOffset.MinValue);
            token.ExpiresIn.Returns(1000);
            token.RefreshToken.Returns("refresh_Token");
            token.TokenType.Returns("token_Type");
            Assert.AreEqual("access_Token", token.AccessToken);
            Assert.AreEqual("account_Id", token.AccountId);
            Assert.AreEqual(DateTimeOffset.MinValue, token.ExpiresAt);
            Assert.AreEqual(1000, token.ExpiresIn);
            Assert.AreEqual("refresh_Token", token.RefreshToken);
            Assert.AreEqual("token_Type", token.TokenType);
        }
    }
}