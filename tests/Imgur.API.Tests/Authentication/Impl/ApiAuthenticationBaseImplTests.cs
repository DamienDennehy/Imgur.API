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
        public void RateLimit_SetByInitialization_IsNotNull()
        {
            var apiAuth = Substitute.ForPartsOf<ApiAuthenticationBase>();
            Assert.IsNotNull(apiAuth.RateLimit);
        }

        [TestMethod]
        public void RateLimit_SetByConstructor1_IsNotNull()
        {
            var apiAuth = Substitute.ForPartsOf<ApiAuthenticationBase>();
            Assert.IsNotNull(apiAuth.RateLimit);
        }

        [TestMethod]
        public void RateLimit_SetByConstructor2_IsNotNull()
        {
            var oAuth = Substitute.For<IOAuth2Authentication>();
            var apiAuth = Substitute.ForPartsOf<ApiAuthenticationBase>(oAuth);
            Assert.IsNotNull(apiAuth.RateLimit);
        }

        [TestMethod]
        public void OAuth2Authentication_SetByConstructor_IsNotNull()
        {
            var oAuth = Substitute.For<IOAuth2Authentication>();
            var apiAuth = Substitute.ForPartsOf<ApiAuthenticationBase>(oAuth);
            Assert.IsNotNull(apiAuth.OAuth2Authentication);
        }
    }
}