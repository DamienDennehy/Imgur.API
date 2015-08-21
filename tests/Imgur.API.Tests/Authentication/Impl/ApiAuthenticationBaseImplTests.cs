using System;
using System.Reflection;
using Imgur.API.Authentication;
using Imgur.API.Authentication.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Authentication.Impl
{
    [TestClass]
    public class ApiAuthenticationBaseImplTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ClientId_SetNullByConstructor_ThrowArgumentNullException()
        {
            try
            {
                var apiAuth = Substitute.ForPartsOf<ApiAuthenticationBase>(null, "ClientSecret");
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ClientSecret_SetNullByConstructor_ThrowArgumentNullException()
        {
            try
            {
                var apiAuth = Substitute.ForPartsOf<ApiAuthenticationBase>("ClientId", null);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OAuth2_SetNullByConstructor_ThrowArgumentNullException()
        {
            try
            {
                var apiAuth = Substitute.ForPartsOf<ApiAuthenticationBase>("ClientId", "ClientSecret", null);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        public void RateLimit_SetByInitialization_IsNotNull()
        {
            var apiAuth = Substitute.ForPartsOf<ApiAuthenticationBase>();
            Assert.IsNotNull(apiAuth.RateLimit);
        }

        [TestMethod]
        public void RateLimit_SetByDefaultConstructor_IsNotNull()
        {
            var apiAuth = Substitute.ForPartsOf<ApiAuthenticationBase>();
            Assert.IsNotNull(apiAuth.RateLimit);
        }

        [TestMethod]
        public void RateLimit_SetByConstructor1_IsNotNull()
        {
            var apiAuth = Substitute.ForPartsOf<ApiAuthenticationBase>("ClientId", "ClientSecret");
            Assert.IsNotNull(apiAuth.RateLimit);
        }

        [TestMethod]
        public void RateLimit_SetByConstructor2_IsNotNull()
        {
            var oAuth = Substitute.For<IOAuth2Authentication>();
            var apiAuth = Substitute.ForPartsOf<ApiAuthenticationBase>("ClientId", "ClientSecret", oAuth);
            Assert.IsNotNull(apiAuth.RateLimit);
        }

        [TestMethod]
        public void OAuth2Authentication_SetByConstructor_IsNotNull()
        {
            var oAuth = Substitute.For<IOAuth2Authentication>();
            var apiAuth = Substitute.ForPartsOf<ApiAuthenticationBase>("ClientId", "ClientSecret", oAuth);
            Assert.IsNotNull(apiAuth.OAuth2Authentication);
        }
    }
}