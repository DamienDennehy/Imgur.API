using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class OAuth2EndpointTests
    {
        [TestMethod]
        public void OAuth2Endpoint_GetAuthorizationUrl_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IOAuth2Endpoint>();
            endpoint.GetAuthorizationUrl(OAuth2ResponseType.Code, null);
            endpoint.Received().GetAuthorizationUrl(OAuth2ResponseType.Code, null);
        }

        [TestMethod]
        public void OAuth2Endpoint_GetTokenByPin_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IOAuth2Endpoint>();
            endpoint.GetTokenByPin("1234");
            endpoint.Received().GetTokenByPin("1234");
        }

        [TestMethod]
        public void OAuth2Endpoint_GetTokenByCode_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IOAuth2Endpoint>();
            endpoint.GetTokenByCode("1234");
            endpoint.Received().GetTokenByCode("1234");
        }

        [TestMethod]
        public void OAuth2Endpoint_GetTokenByRefreshToken_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IOAuth2Endpoint>();
            endpoint.GetTokenByRefreshToken("1234");
            endpoint.Received().GetTokenByRefreshToken("1234");
        }
    }
}
