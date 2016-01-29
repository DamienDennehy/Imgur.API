using Imgur.API.Authentication.Impl;
using Imgur.API.Models.Impl;

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.Mocks
{
    public class MockOAuth2Token
    {
        public OAuth2Token GetOAuth2Token()
        {
            var endpoint = new MockEndpoint(new ImgurClient("A", "B"));
            return endpoint.ProcessEndpointResponse<OAuth2Token>(MockOAuth2EndpointResponses.GetTokenByRefreshToken);
        }
    }
}