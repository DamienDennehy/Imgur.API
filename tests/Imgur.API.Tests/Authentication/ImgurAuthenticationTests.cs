using Imgur.API.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Authentication
{
    [TestClass]
    public class ImgurAuthenticationTests
    {
        [TestMethod]
        public void ClientId_Set_AreEqual()
        {
            var authentication = Substitute.For<IImgurAuthentication>();
            authentication.ClientId.Returns("AbcdE123");
            Assert.AreEqual("AbcdE123", authentication.ClientId);
        }

        [TestMethod]
        public void ClientSecret_Set_AreEqual()
        {
            var authentication = Substitute.For<IImgurAuthentication>();
            authentication.ClientSecret.Returns("Qwerty123");
            Assert.AreEqual("Qwerty123", authentication.ClientSecret);
        }
    }
}