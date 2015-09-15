using System;
using System.Reflection;
using Imgur.API.Authentication;
using Imgur.API.Authentication.Impl;
using Imgur.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Authentication
{
    [TestClass]
    public class ApiClientTests
    {
        [TestMethod]
        public void IOAuth2Token_Set_NotNull()
        {
            var client = Substitute.For<IApiClient>();
            var oAuthToken = Substitute.For<IOAuth2Token>();
            client.OAuth2Token.Returns(oAuthToken);
            Assert.IsNotNull(client.OAuth2Token);
        }

        [TestMethod]
        public void IRateLimit_Set_NotNull()
        {
            var client = Substitute.For<IApiClient>();
            var rateLimit = Substitute.For<IRateLimit>();
            client.RateLimit.Returns(rateLimit);
            Assert.IsNotNull(client.RateLimit);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ClientId_SetNullByConstructor_ThrowArgumentNullException()
        {
            try
            {
                Substitute.ForPartsOf<ApiClientBase>(null, "ClientSecret");
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ClientSecret_SetNullByConstructor_ThrowArgumentNullException()
        {
            try
            {
                Substitute.ForPartsOf<ApiClientBase>("ClientId", null);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void OAuth2_SetNullByConstructor_ThrowArgumentNullException()
        {
            try
            {
                Substitute.ForPartsOf<ApiClientBase>("ClientId", "ClientSecret", null);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        public void RateLimit_SetByInitialization_IsNotNull()
        {
            var client = Substitute.ForPartsOf<ApiClientBase>();
            Assert.IsNotNull(client.RateLimit);
        }

        [TestMethod]
        public void RateLimit_SetByDefaultConstructor_IsNotNull()
        {
            var client = Substitute.ForPartsOf<ApiClientBase>();
            Assert.IsNotNull(client.RateLimit);
        }

        [TestMethod]
        public void RateLimit_SetByConstructor1_IsNotNull()
        {
            var client = Substitute.ForPartsOf<ApiClientBase>("ClientId", "ClientSecret");
            Assert.IsNotNull(client.RateLimit);
        }

        [TestMethod]
        public void RateLimit_SetByConstructor2_IsNotNull()
        {
            var oAuth2Token = Substitute.For<IOAuth2Token>();
            var client = Substitute.ForPartsOf<ApiClientBase>("ClientId", "ClientSecret", oAuth2Token);
            Assert.IsNotNull(client.RateLimit);
        }

        [TestMethod]
        public void OAuth2Token_SetByConstructor_IsNotNull()
        {
            var oAuth2Token = Substitute.For<IOAuth2Token>();
            var client = Substitute.ForPartsOf<ApiClientBase>("ClientId", "ClientSecret", oAuth2Token);
            Assert.IsNotNull(client.OAuth2Token);
        }

        [TestMethod]
        public void OAuth2Token_SetBySetOAuth2Token_IsNotNull()
        {
            var oAuth2Token = Substitute.For<IOAuth2Token>();
            var client = Substitute.ForPartsOf<ApiClientBase>("ClientId", "ClientSecret");

            Assert.IsNull(client.OAuth2Token);
            client.SetOAuth2Token(oAuth2Token);
            Assert.IsNotNull(client.OAuth2Token);
        }

        [TestMethod]
        public void OAuth2Token_SetBySetOAuth2Token_IsNull()
        {
            var oAuth2Token = Substitute.For<IOAuth2Token>();
            var client = Substitute.ForPartsOf<ApiClientBase>("ClientId", "ClientSecret");

            Assert.IsNull(client.OAuth2Token);
            client.SetOAuth2Token(oAuth2Token);
            Assert.IsNotNull(client.OAuth2Token);
            client.SetOAuth2Token(null);
            Assert.IsNull(client.OAuth2Token);
        }
    }
}