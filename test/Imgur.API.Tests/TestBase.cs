using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.Tests.FakeResponses;
using NSubstitute;

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests
{
    public abstract class TestBase
    {
        private IOAuth2Token _token;
        public IOAuth2Token FakeOAuth2Token => GetToken();

        private IOAuth2Token GetToken()
        {
            if (_token != null)
                return _token;

            var endpoint = Substitute.ForPartsOf<EndpointBase>(new ImgurClient("a", "b"));
            _token = endpoint.ProcessEndpointResponse<OAuth2Token>(OAuth2EndpointResponses.GetTokenByCodeAsync);
            return _token;
        }
    }
}