using Imgur.API.Authentication.Impl;
using Imgur.API.Models;

namespace Imgur.API.Tests.Mocks
{
    public class MockApiClient : ApiClientBase
    {
        public MockApiClient(string clientId) : base(clientId)
        {
        }

        public MockApiClient(string clientId, IOAuth2Token oAuth2Token) : base(clientId, oAuth2Token)
        {
        }

        public MockApiClient(string clientId, string clientSecret) : base(clientId, clientSecret)
        {
        }

        public MockApiClient(string clientId, string clientSecret, IOAuth2Token oAuth2Token)
            : base(clientId, clientSecret, oAuth2Token)
        {
        }

        public override string EndpointUrl { get; } = "https://api.imgur.com/3/";
    }
}