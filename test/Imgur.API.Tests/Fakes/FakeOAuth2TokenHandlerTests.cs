using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Fakes
{
    [TestClass]
    public class FakeOAuth2TokenHandlerTests : TestBase
    {
        [TestMethod]
        public void GetOAuth2TokenCodeResponse_AreEqual()
        {
            var token = FakeOAuth2Token;

            Assert.IsNotNull(token);
            Assert.AreEqual("CodeResponse", token.AccessToken);
            Assert.AreEqual(2419200, token.ExpiresIn);
            Assert.AreEqual("bearer", token.TokenType);
            Assert.AreEqual("Bob", token.AccountUsername);
            Assert.AreEqual("45344", token.AccountId);
            Assert.AreEqual("2132d34234jkljj84ce0c16fjkljfsdfdc70", token.RefreshToken);
        }
    }
}