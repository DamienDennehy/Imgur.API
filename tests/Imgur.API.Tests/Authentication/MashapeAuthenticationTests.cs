using System;
using Imgur.API.Authentication;
using Imgur.API.Authentication.Impl;
using Imgur.API.Models;
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

        [TestClass]
        public class MashapeAuthenticationImplTests
        {
            [TestMethod]
            public void ClientId_SetByConstructor_AreEqual()
            {
                var auth = new MashapeAuthentication("ClientId", "ClientSecret", "MashapeKey");
                Assert.AreEqual("ClientId", auth.ClientId);
            }

            [TestMethod]
            [ExpectedException(typeof (ArgumentNullException))]
            public void ClientId_SetNullByConstructor_ThrowArgumentNullException()
            {
                var auth = new MashapeAuthentication(null, "ClientSecret", "MashapeKey");
                Assert.IsNotNull(auth.MashapeKey);
            }

            [TestMethod]
            public void ClientSecret_SetByConstructor_AreEqual()
            {
                var auth = new MashapeAuthentication("ClientId", "ClientSecret", "MashapeKey");
                Assert.AreEqual("ClientSecret", auth.ClientSecret);
            }

            [TestMethod]
            [ExpectedException(typeof (ArgumentNullException))]
            public void ClientSecret_SetNullByConstructor_ThrowArgumentNullException()
            {
                var auth = new MashapeAuthentication("ClientId", null, "MashapeKey");
                Assert.IsNotNull(auth.ClientSecret);
            }

            [TestMethod]
            public void MashapeKey_SetByConstructor_AreEqual()
            {
                var authentication = new MashapeAuthentication("ClientId", "ClientSecret", "MashapeKey");
                Assert.AreEqual("MashapeKey", authentication.MashapeKey);
            }

            [TestMethod]
            [ExpectedException(typeof (ArgumentNullException))]
            public void MashapeKey_SetNullByConstructor_ThrowArgumentNullException()
            {
                var auth = new MashapeAuthentication("ClientId", "ClientSecret", null);
                Assert.IsNotNull(auth.ClientId);
            }

            [TestMethod]
            public void OAuth2Token_SetByConstructor_AreEqual()
            {
                var oAuth2Token = Substitute.For<IOAuth2Token>();
                var auth = new MashapeAuthentication("ClientId", "ClientSecret", "MashapeKey", oAuth2Token);
                Assert.AreEqual(oAuth2Token, auth.OAuth2Token);
            }

            [TestMethod]
            [ExpectedException(typeof (ArgumentNullException))]
            public void OAuth2Token_SetNullByConstructor_ThrowArgumentNullException()
            {
                var auth = new MashapeAuthentication("ClientId", "MashapeKey", null);
                Assert.IsNotNull(auth.OAuth2Token);
            }

            [TestMethod]
            public void RateLimit_SetByConstructor_IsNotNull()
            {
                var auth = new MashapeAuthentication("ClientId", "ClientSecret", "MashapeKey");
                Assert.IsNotNull(auth.RateLimit);
            }
        }
    }
}