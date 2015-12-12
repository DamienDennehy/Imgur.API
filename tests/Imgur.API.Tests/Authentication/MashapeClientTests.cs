using System;
using Imgur.API.Authentication.Impl;
using Imgur.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Authentication
{
    [TestClass]
    public class MashapeClientTests
    {
        [TestMethod]
        public void ClientId_SetByConstructor_AreEqual()
        {
            var client = new MashapeClient("ClientId", "ClientSecret", "MashapeKey");
            Assert.AreEqual("ClientId", client.ClientId);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ClientId_SetNullByConstructor_ThrowArgumentNullException()
        {
            var client = new MashapeClient(null, "ClientSecret", "MashapeKey");
            Assert.IsNotNull(client.MashapeKey);
        }

        [TestMethod]
        public void ClientSecret_SetByConstructor_AreEqual()
        {
            var client = new MashapeClient("ClientId", "ClientSecret", "MashapeKey");
            Assert.AreEqual("ClientSecret", client.ClientSecret);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ClientSecret_SetNullByConstructor_ThrowArgumentNullException()
        {
            var client = new MashapeClient("ClientId", null, "MashapeKey");
            Assert.IsNotNull(client.ClientSecret);
        }

        [TestMethod]
        public void MashapeKey_SetByConstructor_AreEqual()
        {
            var client = new MashapeClient("ClientId", "ClientSecret", "MashapeKey");
            Assert.AreEqual("MashapeKey", client.MashapeKey);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MashapeKey_SetNullByConstructor_ThrowArgumentNullException()
        {
            var client = new MashapeClient("ClientId", "ClientSecret", null);
            Assert.IsNotNull(client.ClientId);
        }

        [TestMethod]
        public void OAuth2Token_SetByConstructor_AreSame()
        {
            var oAuth2Token = Substitute.For<IOAuth2Token>();
            var client = new MashapeClient("ClientId", "ClientSecret", "MashapeKey", oAuth2Token);
            Assert.AreSame(oAuth2Token, client.OAuth2Token);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void OAuth2Token_SetNullByConstructor_ThrowArgumentNullException()
        {
            var client = new MashapeClient("ClientId", "MashapeKey", null);
            Assert.IsNotNull(client.OAuth2Token);
        }

        [TestMethod]
        public void RateLimit_SetByConstructor_IsNotNull()
        {
            var client = new MashapeClient("ClientId", "ClientSecret", "MashapeKey");
            Assert.IsNotNull(client.RateLimit);
        }
    }
}