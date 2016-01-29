using System;
using Imgur.API.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable ExceptionNotDocumented
// ReSharper disable ThrowingSystemException

namespace Imgur.API.Tests.AuthenticationTests
{
    [TestClass]
    public class ApiClientTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ClientId_SetNullByConstructor_ThrowArgumentNullException()
        {
            new MockApiClient(null, "ClientSecret");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ClientSecret_SetNullByConstructor_ThrowArgumentNullException()
        {
            new MockApiClient("ClientId", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OAuth2_SetNullByConstructor_ThrowArgumentNullException()
        {
            new MockApiClient("ClientId", "ClientSecret", null);
        }

        [TestMethod]
        public void OAuth2Token_SetByConstructor_AreSame()
        {
            var oAuth2Token = new MockOAuth2Token().GetOAuth2Token();
            var client = new MockApiClient("ClientId", "ClientSecret", oAuth2Token);
            Assert.AreSame(oAuth2Token, client.OAuth2Token);
        }

        [TestMethod]
        public void OAuth2Token_SetBySetOAuth2Token_AreSame()
        {
            var oAuth2Token = new MockOAuth2Token().GetOAuth2Token();
            var client = new MockApiClient("ClientId", "ClientSecret");

            Assert.IsNull(client.OAuth2Token);
            client.SetOAuth2Token(oAuth2Token);
            Assert.AreSame(oAuth2Token, client.OAuth2Token);
        }

        [TestMethod]
        public void OAuth2Token_SetBySetOAuth2Token_IsNull()
        {
            var oAuth2Token = new MockOAuth2Token().GetOAuth2Token();
            var client = new MockApiClient("ClientId", "ClientSecret");

            Assert.IsNull(client.OAuth2Token);
            client.SetOAuth2Token(oAuth2Token);
            Assert.AreSame(oAuth2Token, client.OAuth2Token);
            client.SetOAuth2Token(null);
            Assert.IsNull(client.OAuth2Token);
        }

        [TestMethod]
        public void RateLimit_SetByConstructor1_IsNotNull()
        {
            var client = new MockApiClient("ClientId", "ClientSecret");
            Assert.IsNotNull(client.RateLimit);
        }

        [TestMethod]
        public void RateLimit_SetByConstructor2_IsNotNull()
        {
            var oAuth2Token = new MockOAuth2Token().GetOAuth2Token();
            var client = new MockApiClient("ClientId", "ClientSecret", oAuth2Token);
            Assert.IsNotNull(client.RateLimit);
        }
    }
}