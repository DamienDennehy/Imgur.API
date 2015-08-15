using Imgur.API.Authentication;
using Imgur.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Authentication
{
    [TestClass]
    public class ApiAuthenticationTests
    {
        [TestMethod]
        public void IOAuth2Authentication_Set_NotNull()
        {
            var apiAuth = Substitute.For<IApiAuthentication>();
            var oAuth = Substitute.For<IOAuth2Authentication>();
            apiAuth.OAuth2Authentication.Returns(oAuth);
            Assert.IsNotNull(apiAuth.OAuth2Authentication);
        }

        [TestMethod]
        public void IRateLimit_Set_NotNull()
        {
            var apiAuth = Substitute.For<IApiAuthentication>();
            var rateLimit = Substitute.For<IRateLimit>();
            apiAuth.RateLimit.Returns(rateLimit);
            Assert.IsNotNull(apiAuth.RateLimit);
        }
    }
}