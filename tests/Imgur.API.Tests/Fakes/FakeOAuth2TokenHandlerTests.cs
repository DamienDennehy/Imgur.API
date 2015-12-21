using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Fakes
{
    [TestClass]
    public class FakeOAuth2TokenHandlerTests
    {
        [TestMethod]
        public void GetOAuth2TokenCodeResponse_AreEqual()
        {
            var fakeOAuth2TokenHandler = new FakeOAuth2TokenHandler();
            var token = fakeOAuth2TokenHandler.GetOAuth2TokenCodeResponse();

            Assert.IsNotNull(token);
            Assert.AreEqual("45344", token.AccountId);
            Assert.AreEqual("CodeResponse", token.AccessToken);
            Assert.AreEqual(3600, token.ExpiresIn);
            Assert.AreEqual("bearer", token.TokenType);
            Assert.AreEqual("2132d34234jkljj84ce0c16fjkljfsdfdc70", token.RefreshToken);
        }
    }
}