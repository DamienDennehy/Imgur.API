using Imgur.API.Authentication;
using Imgur.API.Models;
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
    }
}