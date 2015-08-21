using Imgur.API.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Authentication
{
    [TestClass]
    public class MashapeAuthenticationTests
    {
        [TestMethod]
        public void ClientId_Set_AreEqual()
        {
            var auth = Substitute.For<IMashapeAuthentication>();
            auth.ClientId.Returns("AbcdE123P");
            Assert.AreEqual("AbcdE123P", auth.ClientId);
        }

        [TestMethod]
        public void ClientSecret_Set_AreEqual()
        {
            var auth = Substitute.For<IMashapeAuthentication>();
            auth.ClientSecret.Returns("Qwerty123");
            Assert.AreEqual("Qwerty123", auth.ClientSecret);
        }

        [TestMethod]
        public void MashapeKey_Set_AreEqual()
        {
            var auth = Substitute.For<IMashapeAuthentication>();
            auth.MashapeKey.Returns("Psfdsf7676");
            Assert.AreEqual("Psfdsf7676", auth.MashapeKey);
        }
    }
}