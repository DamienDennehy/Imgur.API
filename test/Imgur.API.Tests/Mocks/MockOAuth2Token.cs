using Imgur.API.Authentication;
using Imgur.API.Models;
using System.Net.Http;

namespace Imgur.API.Tests.Mocks
{
    public static class MockOAuth2Token
    {
        public static OAuth2Token GetOAuth2Token()
        {
            var endpoint = new MockEndpoint(new ImgurClient("A", "B"));
            var response = new HttpResponseMessage
            {
                Content = new StringContent(MockOAuth2EndpointResponses.GetTokenByRefreshToken)
            };
            return endpoint.ProcessEndpointResponse<OAuth2Token>(response);
        }
    }
}