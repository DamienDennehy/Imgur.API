using System.Net.Http;
using Imgur.API.Authentication.Impl;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.Tests.Mocks;

namespace Imgur.API.Tests
{
    public abstract class TestBase
    {
        private IOAuth2Token _token;
        public IOAuth2Token MockOAuth2Token => GetToken();

        private IOAuth2Token GetToken()
        {
            if (_token != null)
                return _token;

            var endpoint = new MockEndpoint(new ImgurClient("a", "b"));
            var response = new HttpResponseMessage
            {
                Content = new StringContent(MockOAuth2EndpointResponses.GetTokenByCode)
            };
            _token = endpoint.ProcessEndpointResponse<OAuth2Token>(response);
            return _token;
        }
    }
}