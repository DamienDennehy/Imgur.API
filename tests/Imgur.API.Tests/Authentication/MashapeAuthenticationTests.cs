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
            var authentication = Substitute.For<IMashapeAuthentication>();
            authentication.ClientId.Returns("AbcdE123P");
            Assert.AreEqual("AbcdE123P", authentication.ClientId);
        }

        [TestMethod]
        public void MashapeKey_Set_AreEqual()
        {
            var authentication = Substitute.For<IMashapeAuthentication>();
            authentication.MashapeKey.Returns("Psfdsf7676");
            Assert.AreEqual("Psfdsf7676", authentication.MashapeKey);
        }
    }
}