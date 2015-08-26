using System;
using Imgur.API.Authentication;
using Imgur.API.Authentication.Impl;
using Imgur.API.Models;
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
            var auth = Substitute.For<IImgurAuthentication>();
            auth.ClientId.Returns("AbcdE123");
            Assert.AreEqual("AbcdE123", auth.ClientId);
        }

        [TestMethod]
        public void ClientSecret_Set_AreEqual()
        {
            var auth = Substitute.For<IImgurAuthentication>();
            auth.ClientSecret.Returns("Qwerty123");
            Assert.AreEqual("Qwerty123", auth.ClientSecret);
        }

        [TestMethod]
        public void ClientId_SetByConstructor_AreEqual()
        {
            var auth = new ImgurAuthentication("ClientId", "ClientSecret");
            Assert.AreEqual("ClientId", auth.ClientId);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ClientId_SetNullByConstructor_ThrowArgumentNullException()
        {
            var auth = new ImgurAuthentication(null, "ClientSecret");
            Assert.IsNotNull(auth.ClientSecret);
        }

        [TestMethod]
        public void ClientSecret_SetByConstructor_AreEqual()
        {
            var auth = new ImgurAuthentication("ClientId", "ClientSecret");
            Assert.AreEqual("ClientSecret", auth.ClientSecret);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ClientSecret_SetNullByConstructor_ThrowArgumentNullException()
        {
            var auth = new ImgurAuthentication("ClientId", null);
            Assert.IsNotNull(auth.ClientId);
        }

        [TestMethod]
        public void OAuth2Token_SetByConstructor_AreEqual()
        {
            var oAuth2Token = Substitute.For<IOAuth2Token>();
            var auth = new ImgurAuthentication("ClientId", "ClientSecret", oAuth2Token);
            Assert.AreEqual(oAuth2Token, auth.OAuth2Token);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void OAuth2Token_SetNullByConstructor_ThrowArgumentNullException()
        {
            var auth = new ImgurAuthentication("ClientId", "ClientSecret", null);
            Assert.IsNotNull(auth.OAuth2Token);
        }

        [TestMethod]
        public void RateLimit_SetByInitialization_IsNotNull()
        {
            var auth = new ImgurAuthentication("ClientId", "ClientSecret");
            Assert.IsNotNull(auth.RateLimit);
        }
    }
}