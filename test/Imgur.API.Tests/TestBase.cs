using Imgur.API.Authentication;
using Imgur.API.Models;
using Imgur.API.Tests.Mocks;
using System.Net.Http;

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