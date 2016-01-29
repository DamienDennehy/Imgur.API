using System;
using Imgur.API.Authentication.Impl;
using Imgur.API.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.AuthenticationTests
{
    [TestClass]
    public class ImgurClientTests
    {
        [TestMethod]
        public void ClientId_SetByConstructor_AreEqual()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            Assert.AreEqual("ClientId", client.ClientId);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ClientId_SetNullByConstructor_ThrowArgumentNullException()
        {
            var client = new ImgurClient(null, "ClientSecret");
            Assert.IsNotNull(client.ClientSecret);
        }

        [TestMethod]
        public void ClientSecret_SetByConstructor_AreEqual()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            Assert.AreEqual("ClientSecret", client.ClientSecret);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ClientSecret_SetNullByConstructor_ThrowArgumentNullException()
        {
            var client = new ImgurClient("ClientId", null);
            Assert.IsNotNull(client.ClientId);
        }

        [TestMethod]
        public void EndpointUrl_IsImgurUrl()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            Assert.AreEqual("https://api.imgur.com/3/", client.EndpointUrl);
        }

        [TestMethod]
        public void OAuth2Token_SetByConstructor_AreSame()
        {
            var oAuth2Token = new MockOAuth2Token().GetOAuth2Token();
            var client = new ImgurClient("ClientId", "ClientSecret", oAuth2Token);
            Assert.AreSame(oAuth2Token, client.OAuth2Token);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void OAuth2Token_SetNullByConstructor_ThrowArgumentNullException()
        {
            var client = new ImgurClient("ClientId", "ClientSecret", null);
            Assert.IsNotNull(client.OAuth2Token);
        }

        [TestMethod]
        public void RateLimit_SetByInitialization_IsNotNull()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            Assert.IsNotNull(client.RateLimit);
        }
    }
}