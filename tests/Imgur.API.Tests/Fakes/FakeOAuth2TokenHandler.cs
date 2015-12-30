using Imgur.API.Endpoints.Impl;
using Imgur.API.Models.Impl;
using Imgur.API.Tests.FakeResponses;
using NSubstitute;

namespace Imgur.API.Tests.Fakes
{
    public class FakeOAuth2TokenHandler
    {
        public OAuth2Token GetOAuth2TokenCodeResponse()
        {
            var endpoint = Substitute.For<EndpointBase>();
            return endpoint.ProcessEndpointResponse<OAuth2Token>(OAuth2EndpointResponses.GetTokenByRefreshTokenAsync);
        }
    }
}