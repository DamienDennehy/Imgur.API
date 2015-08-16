using System;
using Imgur.API.Authentication;
using Imgur.API.Authentication.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Authentication.Impl
{
    [TestClass]
    public class MashapeAuthenticationImplTests
    {
        [TestMethod]
        public void ClientId_SetByConstructor_AreEqual()
        {
            var auth = new MashapeAuthentication("ClientId", "MashapeKey");
            Assert.AreEqual("ClientId", auth.ClientId);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ClientId_SetNullByConstructor_ThrowArgumentNullException()
        {
            var auth = new MashapeAuthentication(null, "MashapeKey");
            Assert.IsNotNull(auth.MashapeKey);
        }

        [TestMethod]
        public void MashapeKey_SetByConstructor_AreEqual()
        {
            var authentication = new MashapeAuthentication("ClientId", "MashapeKey");
            Assert.AreEqual("MashapeKey", authentication.MashapeKey);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MashapeKey_SetNullByConstructor_ThrowArgumentNullException()
        {
            var auth = new MashapeAuthentication("ClientId", null);
            Assert.IsNotNull(auth.ClientId);
        }

        [TestMethod]
        public void OAuth2Authentication_SetByConstructor_AreEqual()
        {
            var oAuth2 = Substitute.For<IOAuth2Authentication>();
            var auth = new MashapeAuthentication("ClientId", "MashapeKey", oAuth2);
            Assert.AreEqual(oAuth2, auth.OAuth2Authentication);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void OAuth2Authentication_SetNullByConstructor_ThrowArgumentNullException()
        {
            var auth = new MashapeAuthentication("ClientId", "MashapeKey", null);
            Assert.IsNotNull(auth.OAuth2Authentication);
        }

        [TestMethod]
        public void RateLimit_SetByConstructor_IsNotNull()
        {
            var auth = new MashapeAuthentication("ClientId", "MashapeKey");
            Assert.IsNotNull(auth.RateLimit);
        }
    }
}